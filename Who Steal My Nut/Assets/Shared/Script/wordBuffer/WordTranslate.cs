using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WordTranslate : MonoBehaviour
{
    /*Position*/
    private Vector3 mousePos;
    private Vector3 worldPos;
    private Vector3Int cellPos;

    /*Important Static*/
    private Vector3Int bigALeftBottom = new Vector3Int(-56, 25, 0);
    private Vector3Int bigLLeftBottom = new Vector3Int(-56, -62, 0);
    private Vector3Int windowLeftTop = new Vector3Int(-111, 55, 0);
    private Vector3Int windowRightTop = new Vector3Int(105, 55, 0);
    private Vector3Int windowLeftBottom = new Vector3Int(-111, -70, 0);
    private Vector3Int windowRightBottom = new Vector3Int(105, -70, 0);
    private string inputStr = 
        "As you can see, this technology fully explains people's spirit of exploration. " 
        +"Because this is an advancing technology and its convenience will eventually benefit ourselves."
        +"_include_iostream_                         printf_HelloWorld._;";
    private char[] inputCharArray;

    /*Grid*/
    private Grid grid;
    private Tilemap tilemapSource;
    private Tilemap tilemapGoal;
    private Tilemap tilemap;

    /*Temp*/
    private int interyalVertical = 0;
    private int interyalHorizontal = 0;

    /*Auto*/
    private bool isWaitingRend = true;
    private bool isTheFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponentInParent<Grid>();
        tilemapSource = GameObject.Find("wordAlphabet").GetComponentInParent<Tilemap>();
        tilemapGoal = GameObject.Find("wordRenderer").GetComponentInParent<Tilemap>();
        tilemap = gameObject.GetComponentInParent<Tilemap>();
        inputCharArray = inputStr.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        //mousePos = Input.mousePosition;
        //worldPos = Camera.main.ScreenToWorldPoint(new Vector3
        //    (mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));
        //cellPos = grid.WorldToCell(worldPos);
        //Debug.Log(cellPos);
        if(isWaitingRend)
        {
            Rend();
            isWaitingRend =false;
        }
    }

    private void Rend()
    {
        Vector3Int nextRendPos = new Vector3Int(0, 0, 0);
        Vector3Int sourceLeftBottom;
        foreach (var letter in inputCharArray)
        {
            nextRendPos = RendPointer(nextRendPos, letter);
            sourceLeftBottom = CharToCellPos(letter);
            for(int i = 0; i <= 6; i++)
            {
                for (int j = -2; j <= 5; j++)
                    if (tilemapSource.GetTile(sourceLeftBottom + new Vector3Int(i, j, 0)) != null)
                        tilemap.SetTile
                            (nextRendPos + new Vector3Int(i, j, 0),
                             tilemapSource.GetTile(sourceLeftBottom + new Vector3Int(i, j, 0)));
            }
        }
    }

    private Vector3Int RendPointer(Vector3Int lastRendPos, char letter)
    {
        int appliableYValue = 0;
        int appliableXValue = 0;
        int blankRate = 0;
        bool isModify = false;
        Vector3Int space = new Vector3Int(0, 0, 0);
        if (letter == ' ') space = new Vector3Int(2, 0, 0);
        if (isTheFirst)
        {
            isTheFirst = false;
            return windowLeftTop;
        }
        else
        {
            for (int i = 0; i <= 7; i++)
            {
                for (int j = -2; j <= 5; j++)
                {
                    if (tilemap.GetTile(lastRendPos + new Vector3Int(i, j, 0)) == null) blankRate++;
                }
                if (blankRate == 8 && !isModify)
                {
                    appliableXValue = lastRendPos.x + i + 1;
                    if (appliableXValue > 105)
                    {
                        appliableXValue = windowLeftTop.x;
                        appliableYValue = lastRendPos.y - 9;
                        space = new Vector3Int(0, 0, 0);
                    }
                    else appliableYValue = lastRendPos.y;
                    isModify = true;
                }
                blankRate = 0;
            }
        }
        return new Vector3Int(appliableXValue, appliableYValue, lastRendPos.z) + space;
    }

    private Vector3Int CharToCellPos(char letter)
    {
        if (letter >= 'A' && letter <= 'K')
        {
            interyalHorizontal = 0;
            interyalVertical = letter - 'A';
            return bigALeftBottom + new Vector3Int(interyalHorizontal * 7, -interyalVertical * 8, 0);
        }
        else if (letter > 'L' && letter <= 'Z')
        {
            interyalHorizontal = 0;
            interyalVertical = letter - 'L';
            return bigLLeftBottom + new Vector3Int(interyalHorizontal * 7, -interyalVertical * 8, 0);
        }
        else if (letter >= 'a' && letter <= 'k')
        {
            interyalHorizontal = 1;
            interyalVertical = letter - 'a';
            return bigALeftBottom + new Vector3Int(interyalHorizontal * 7, -interyalVertical * 8, 0);
        }
        else if (letter > 'l' && letter <= 'z')
        {
            interyalHorizontal = 1;
            interyalVertical = letter - 'l';
            return bigLLeftBottom + new Vector3Int(interyalHorizontal * 7, -interyalVertical * 8, 0);
        }
        else if (letter == 'L')
            return bigLLeftBottom + new Vector3Int(16, 0, 0);
        else if (letter == 'l')
            return bigLLeftBottom + new Vector3Int(23, 0, 0);
        else if (letter == '.')
            return bigALeftBottom + new Vector3Int(2 * 7, 0, 0);
        else if (letter == ',')
            return bigALeftBottom + new Vector3Int(2 * 7, -8, 0);
        else if (letter == ';')
            return bigALeftBottom + new Vector3Int(2 * 7, -16, 0);
        else if (letter == ' ')
            return bigALeftBottom + new Vector3Int(3 * 7, -16, 0);
        else if (letter == '_')
            return bigALeftBottom + new Vector3Int(2 * 7, -24, 0);
        else return bigALeftBottom;
    }
}
