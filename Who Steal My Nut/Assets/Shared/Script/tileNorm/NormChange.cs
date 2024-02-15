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
    private float streamingSpeed = 1f;

    /*Grid*/
    private Grid grid;
    private Tilemap tilemap;

    /*Position*/
    private Vector3 mousePos;
    private Vector3Int cellPos;
    private Vector3 worldPos;

    /*FrameList*/
    private Vector3Int frameOne = new Vector3Int(-8, -1, 0);
    private Vector3Int frameTwo = new Vector3Int(-7, -2, 0);
    private Vector3Int frameThree = new Vector3Int(-6, -3, 0);
    private Vector3Int frameFour = new Vector3Int(-5, -4, 0);
    private Vector3Int frameFive = new Vector3Int(-4, -5, 0);
    private Vector3Int[] frameList; // Carry Vector3Ints above
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
        //mousePos = Input.mousePosition;
        //worldPos = Camera.main.ScreenToWorldPoint(new Vector3
        //    (mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));
        //cellPos = grid.WorldToCell(worldPos);
        if (Mathf.Floor(timer) % 3 != 0)
        {
            isTimeAllowPlay = true;
            if (Mathf.Floor(timer * 100) % 100 == 0) isFinishRow = false;
        }
        else isTimeAllowPlay = false;
        isNotPlay = true;
        FrameByTime();
        Debug.Log(frameDes);
    }

    private void FrameByTime()
    {
        if (Mathf.Floor(timer * 10) % 10 == 0 && isForward && isTimeAllowPlay && isNotPlay && !isFinishRow)
        {
            for (i = -10; i <= 10; i++)
            {
                for (j = -10; j <= 10; j++)
                {
                    if (i + j != -9) TilePlayForward(new Vector3Int(i, j, 0));
                }
            }
            isNotPlay = false;
        }
        if (Mathf.Floor(timer * 10) % 10 == 0 && !isForward && isTimeAllowPlay && isNotPlay && !isFinishRow)
        {
            for (i = -10; i <= 10; i++)
            {
                for (j = -10; j <= 10; j++)
                {
                    if (i + j != -9) TilePlayBack(new Vector3Int(i, j, 0));
                }
            }
            isNotPlay = false;
        }
    }

    private void TilePlayForward(Vector3Int pos)
    {
        for (frameCount = 0; frameCount < frameList.Length; frameCount++)
        {
            if (tilemap.GetTile(pos) == tilemap.GetTile(frameList[frameCount]) && !isMatch)
            {
                if (frameCount == frameList.Length - 1)
                {
                    frameDes = frameList[frameList.Length - 1];
                    isFinishRow = true;
                    isForward = false;
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
        for (frameCount = frameList.Length - 1; frameCount >= 0; frameCount--)
        {
            if (tilemap.GetTile(pos) == tilemap.GetTile(frameList[frameCount]) && !isMatch)
            {
                if (frameCount == 0)
                {
                    frameDes = frameList[0];
                    isFinishRow = true;
                    isForward = true;
                }
                else frameDes = frameList[frameCount - 1];
                isMatch = true;
            }
        }
        if (tilemap.GetTile(pos) != null) tilemap.SetTile(pos, tilemap.GetTile(frameDes));
        isMatch = false;
    }
}
