using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FrontRend : MonoBehaviour
{
    /*Important Static*/
    private Vector3Int bigALeftBottom = new Vector3Int(-56, 25, 0);
    private Vector3Int bigLLeftBottom = new Vector3Int(-56, -62, 0);
    public Vector3Int windowLeftTop = new Vector3Int(-111, 55, 0);
    public Vector3Int windowRightTop = new Vector3Int(105, 55, 0);
    public Vector3Int windowLeftBottom = new Vector3Int(-111, -70, 0);
    public Vector3Int windowRightBottom = new Vector3Int(105, -70, 0);
    public Vector3Int rendLeftTop= new Vector3Int(-111, 61, 0);
    public Vector3Int rendRightBottom = new Vector3Int(113, -72, 0);
    public Vector3Int fixTheError = new Vector3Int(2, 0, 0);// Bias

    /*Grid*/
    private Tilemap wordRenderer;
    private Tilemap wordBuffer;

    /*Auto*/
    private float timer = 0f;
    private bool isFirstRequest = true;
    private bool isCanPageDown = false;
    private bool isUpRequest = false;
    private bool isDownRequest = false;
    private Vector3Int rendererStartPointer = new Vector3Int(0, 0, 0);//Renderer Pointer

    // Start is called before the first frame update
    void Start()
    {
        wordRenderer = gameObject.GetComponent<Tilemap>();
        wordBuffer = GameObject.Find("wordBuffer").GetComponent<Tilemap>();
        //Debug.Log("wordRenderer Try The First Rend!");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f && isFirstRequest)
        {
            Flush();
            Rend();
            isFirstRequest = false;
        }// Rend at the beginning ( without request )
        if(GameObject.Find("wordBuffer").
            GetComponent<WordTranslate >().isWaitingFrontRend == true)
        {
            Flush();
            rendererStartPointer = new Vector3Int(0, 0, 0);
            Rend();
            GameObject.Find("wordBuffer").
                GetComponent<WordTranslate>().isWaitingFrontRend = false;
        }// Rend ( with request )
        if (GameObject.Find("WelcomeSceneSortingOrderConfig").
                GetComponent<SortWelcomeSceneObject>().isAlert == true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && !isDownRequest)
            {
                isDownRequest = true;
                isCanPageDown = !CheckBottom();// Check passage's end
                Flush();
                if (isCanPageDown)
                    rendererStartPointer -= new Vector3Int(0, 10, 0);
                Rend();
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && !isUpRequest)
            {
                isUpRequest = true;
                Flush();
                if (rendererStartPointer != new Vector3Int(0, 0, 0))
                    // Check passage's beginning
                    rendererStartPointer += new Vector3Int(0, 10, 0);
                Rend();
            }
            isDownRequest = false;
            isUpRequest =false;
        }
    }

    private void Flush()// Clean Screen
    {
        for (int i = windowLeftTop.x - 1; i <= windowRightTop.x + 8; i++)
        {
            for (int j = windowLeftBottom.y - 1000; j <= windowLeftTop.y + 1000; j++)
            {
                wordRenderer.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
    }

    private void Rend()// Copy and Paste Buffer
    {
        for (int i = 0; i <= rendRightBottom.x - rendLeftTop.x; i++)
        {
            for (int j = 0; j <= rendLeftTop.y - rendRightBottom.y; j++)
            {
                wordRenderer.SetTile(rendLeftTop + new Vector3Int(i, -j, 0) + fixTheError,
                    wordBuffer.GetTile
                    (rendLeftTop + new Vector3Int(i, -j, 0) + rendererStartPointer));
            }
        }
    }

    private bool CheckBottom()// If blankRate is high, identify it as end
    {
        int blankRate = 0;
        for (int i = 0; i <= 9; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
                if (wordRenderer.GetTile(windowLeftBottom + new Vector3Int(i, j, 0)) == null)
                    blankRate++;
            }
        }
        Debug.Log(blankRate);
        if (blankRate == 100)
        {
            return true;
        }
        else return false;
    }
}
