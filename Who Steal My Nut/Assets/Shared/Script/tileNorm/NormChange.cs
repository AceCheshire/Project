using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NormChange : MonoBehaviour
{
    /*Matrix*/
    private int i = 0;
    private int j = 0;

    /*Time*/
    private float timer = 0f;
    private float streamingSpeed = 0.5f;
    private float timerRecord = 0f;

    /*Grid*/
    private Grid grid;
    private Tilemap tilemap;

    /*FrameList*/
    private Vector3Int frameOne = new Vector3Int(-8, -1, 0);
    private Vector3Int frameTwo = new Vector3Int(-7, -2, 0);
    private Vector3Int frameThree = new Vector3Int(-6, -3, 0);
    private Vector3Int frameFour = new Vector3Int(-5, -4, 0);
    private Vector3Int frameFive = new Vector3Int(-4, -5, 0);
    public Vector3Int[] frameList; // Carry Vector3Ints above
    private int frameCount; // Count frameList 
    private Vector3Int frameDes; // The next frame that will be presented
    private bool isForward = true;
    private bool isTimeAllowPlay = false;
    private bool isNotPlay = true;
    private bool isMatch = false;
    private bool isFinishRow = false;

    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.transform.parent.GetComponent<Grid>();
        tilemap = gameObject.GetComponent<Tilemap>();
        frameList = new Vector3Int[] { frameOne, frameTwo, frameThree, frameFour, frameFive };
        Debug.Log("NormChange Start!");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * streamingSpeed;
        if (timerRecord < timer) timerRecord = Mathf.Floor(timer * 10) / 10;// Allow 0.1 'sec' per frame
        if (Mathf.Floor(timer) % 3 != 0)
        {
            isTimeAllowPlay = true;// Allow 3.0 'sec' per  two rows ( forward - back - stay )
            if (Mathf.Floor(timer * 100) % 100 == 0) isFinishRow = false;// 1.0 sec forward 2.0 sec back
        }
        else isTimeAllowPlay = false;
        isNotPlay = true;// Control that only forward or back instead of ( forward + back ) = stay
        FrameByTime();
    }

    private void FrameByTime()
    {
        if (timerRecord < timer && isForward && isTimeAllowPlay && isNotPlay && !isFinishRow)
        {
            for (i = -10; i <= 10; i++)
            {
                for (j = -10; j <= 10; j++)
                {
                    if (i + j != -9) TilePlayForward(new Vector3Int(i, j, 0));// Marix Operation
                }
            }
            isNotPlay = false;
            timerRecord += 0.1f;
        }
        if (timerRecord < timer && !isForward && isTimeAllowPlay && isNotPlay && !isFinishRow)
        {
            for (i = -10; i <= 10; i++)
            {
                for (j = -10; j <= 10; j++)
                {
                    if (i + j != -9) TilePlayBack(new Vector3Int(i, j, 0));// Matrix Operation
                }
            }
            isNotPlay = false;
            timerRecord += 0.1f;
        }
    }

    private void TilePlayForward(Vector3Int pos)
    {
        for (frameCount = 0; frameCount < frameList.Length; frameCount++)// Find next frame
        {
            if (tilemap.GetTile(pos) == tilemap.GetTile(frameList[frameCount]) && !isMatch)
            {
                if (frameCount == frameList.Length - 1)
                {
                    frameDes = frameList[frameList.Length - 1];
                    isFinishRow = true;
                    isForward = false;// Change direction
                }
                else frameDes = frameList[frameCount + 1];
                isMatch = true;
            }
        }
        if (tilemap.GetTile(pos) != null) tilemap.SetTile(pos, tilemap.GetTile(frameDes));
        isMatch = false;
    }

    private void TilePlayBack(Vector3Int pos)
    {
        for (frameCount = frameList.Length - 1; frameCount >= 0; frameCount--)// Find next frame
        {
            if (tilemap.GetTile(pos) == tilemap.GetTile(frameList[frameCount]) && !isMatch)
            {
                if (frameCount == 0)
                {
                    frameDes = frameList[0];
                    isFinishRow = true;
                    isForward = true;// Change direction
                }
                else frameDes = frameList[frameCount - 1];
                isMatch = true;
            }
        }
        if (tilemap.GetTile(pos) != null) tilemap.SetTile(pos, tilemap.GetTile(frameDes));
        isMatch = false;
    }
}
