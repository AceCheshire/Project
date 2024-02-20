using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.Tilemaps;

/*### 这东西完全运行不了嘛 ###*/
/*### 另外，为啥 play 之后 nut 的坐标变成 (0,0,0) 了 ###*/
/*### vocal ，play 之后那些没放上去的 tile 为撒又出来了 ###*/
/*### 允许重写所有方法 ###*/
public class StageOneStatus : MonoBehaviour
{
    /*Lock & Unlock*/
    public bool isUnlockSync = false;
    public bool isUnlockXyz = false;
    public bool isUnlockLink = false;
    public bool isCanEarthCreateMode = true;
    public bool isCanEnchantCreateMode = false;
    public bool isEarthCreateMode = false;
    public bool isEnchantCreateMode = false;
    public bool isVisitMode = true;
    public bool isRunningMode = false;

    /*StageErea*/
    public Vector3Int stageOneCellRelativeOrigin = new Vector3Int(0, 0, 0);
    public Vector3Int stageOneCellRelativeEnd = new Vector3Int(5, 0, 0);
    public Vector3 stageOneWorldOriginPoint = new Vector3(-6.5f, 3.6f, 0);
    public Vector2Int stageOneEarthArea = new Vector2Int(0, 0);
    public Vector3 birthHeight = new Vector3(0, 3f, 0);
    public float bounceAngle = 60;
    private float heightDistanceRate;
    public float gravityScale = 0.8f;
    public float deltaTime = 0.05f;
    private float allowBias = 0.01f;

    /*RunningMode Position*/
    private bool isWaitingFirstCollide = true;
    private bool isWaitingBirth = true;
    private Vector3 curWorldPos;
    private Vector3 desWorldPos;
    private Vector3 topWorldPos;
    private Vector3 nextWorldPos;
    private int runningDirection = 1;
    private bool isWaitingRun = true;

    /*Timer*/
    private float timer = 0f;
    private float delayer = 0f;

    /*Unital Grid*/
    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        heightDistanceRate = Mathf.Tan(Mathf.PI * bounceAngle / 180) * 0.5f;
        grid = GameObject.Find("mainGround").GetComponent<Grid>();
        Debug.Log("StageOneConfig Open!");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        curWorldPos = GameObject.Find("nut").transform.position;
        isWaitingRun = true;
        if (timer > delayer)
        {
            if (isRunningMode && isWaitingFirstCollide) FirstDown();
            // Special Situation: the first falling
            if (PosSimilar(curWorldPos, desWorldPos)) // Check Collision
                UpdateDestination(desWorldPos);// Change nut's destination
            Bounce();// Try to carry out normal bounce
            delayer += deltaTime;
        }// Action is carried out per [no Time.-]deltaTime
    }

    private void Bounce()
    {
        if (isWaitingRun)
        {
            GameObject.Find("nut").transform.position = nextWorldPos;
            isWaitingRun = false;
        }
    }

    private int DesMatch(Vector3Int desPos, Tilemap tilemap)
    {
        Tilemap sourcemap;
        sourcemap = GameObject.Find("tileFloatGround").GetComponent<Tilemap>();
        Vector3Int[] frameList = tilemap.GetComponent<NormChange>().frameList;
        for (int frameCount = frameList.Length - 1; frameCount >= 0; frameCount--)
            // Find next frame
        {
            if (tilemap.GetTile(desPos) == tilemap.GetTile(frameList[frameCount]))
            {
                return frameCount;
            }
        }
        return -1;
    }// Check destination's Anime Status

    private bool PosSimilar(Vector3 curPos, Vector3 desPos)
    {
        Vector3Int curCellPos;
        Vector3Int desCellPos;
        Tilemap tilemap;
        tilemap = GameObject.Find("tileNormGround").GetComponent<Tilemap>();
        curCellPos = grid.WorldToCell(curPos);
        desCellPos = grid.WorldToCell(desPos);
        if (curCellPos == desCellPos 
            && curPos.y <= desPos.y - DesMatch(desCellPos, tilemap) * allowBias)
            return true;
        else return false;
    }// Check if there is collision

    private float GravityToDistance(float gravity, float height)
    // Down is already - gone height, up is left - to - go height
    {
        float verticalCurVelocity = Mathf.Sqrt(2 * Mathf.Abs(height) * gravity);
        if (height > 0) 
            return verticalCurVelocity * deltaTime + gravity * deltaTime * deltaTime / 2f;
        // Down
        else return verticalCurVelocity * deltaTime - gravity * deltaTime * deltaTime / 2f;
        // Up
    }// Get vertical move distance of next action

    private void FirstDown()
    {
        if (isWaitingBirth)
        {
            GameObject.Find("nut").transform.position = stageOneWorldOriginPoint + birthHeight;
            isWaitingBirth = false;// Teleport to the Birth Position
        }
        if (!isWaitingBirth)
        {
            GameObject.Find("nut").transform.position
                -= new Vector3(0, GravityToDistance(gravityScale, birthHeight.y), 0);
            isWaitingRun = false;
            if (GameObject.Find("nut").transform.position.y <= stageOneWorldOriginPoint.y)
            {
                GameObject.Find("nut").transform.position = stageOneWorldOriginPoint;
                UpdateDestination(stageOneWorldOriginPoint);
                isWaitingFirstCollide = false;// Vertically falling
            }
        }
    }

    private void UpdateDestination(Vector3 curPos)
    {
        Tilemap tilemap;
        Vector3Int curCellPos;
        tilemap = GameObject.Find("tileNormGround").GetComponent<Tilemap>();
        curCellPos = grid.WorldToCell(curPos);
        if (runningDirection == 1)
        {
            topWorldPos = desWorldPos +
                new Vector3(0.65f, -0.325f + Mathf.Sqrt(5) * 0.325f * heightDistanceRate, 0);
            desWorldPos = desWorldPos + new Vector3(1.3f, -0.65f, 0);// Update desWorldPos
        }
        else if (runningDirection == 2) {; }
        else if (runningDirection == 3) {; }
        else if (runningDirection == 4) {; }
    }

    private void UpdateNextPos(Vector3 curPos)
    {
        bool isUp = true;
        float normGetTopTime = 
            Mathf.Sqrt(2 * Mathf.Sqrt(5) * 0.325f * heightDistanceRate / gravityScale) * 2;
        if (runningDirection == 1 && curPos.x < topWorldPos.x) isUp = true;
        else isUp = false;
        if (isUp)
        {
            nextWorldPos += new Vector3
                (0, GravityToDistance(gravityScale, curPos.y - topWorldPos.y), 0);
        }
        else
        {
            nextWorldPos -= new Vector3
               (0, GravityToDistance(gravityScale, -curPos.y + topWorldPos.y), 0);
        }
        nextWorldPos += new Vector3
            (deltaTime / normGetTopTime * 1.3f, deltaTime / normGetTopTime * -0.65f, 0);
    }// Calculate the next position of the next action when Normal Bounce
}