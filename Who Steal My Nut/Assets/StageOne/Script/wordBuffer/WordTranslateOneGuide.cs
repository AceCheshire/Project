using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WordTranslateOneGuide : MonoBehaviour
{

    /*Important Static*/
    private Vector3Int bigALeftBottom = new Vector3Int(-56, 25, 0);
    private Vector3Int bigLLeftBottom = new Vector3Int(-56, -62, 0);
    public Vector3Int windowLeftTop = new Vector3Int(-111, 55, 0);
    public Vector3Int windowRightTop = new Vector3Int(300, 55, 0);
    public Vector3Int windowLeftBottom = new Vector3Int(-111, -70, 0);
    public Vector3Int windowRightBottom = new Vector3Int(300, -70, 0);
    public string inputStr =
        "#include<stdio.h>\n"
        + "int main()\n"
        + "{\n"
        + "    printf(\"Hello World!\");\n"
        + "    return 0;\n"
        + "}";// Content ( Continuous ) to be rended
    private char[] inputCharArray;// Content ( Discrete ) to be rended

    /*Grid*/
    private Grid grid;
    private Tilemap tilemapSource;
    private Tilemap tilemapGoal;
    private Tilemap tilemap;

    /*Temp*/
    private int intervalVertical = 0;// Relative vertical distance in Grid
    private int intervalHorizontal = 0;// Relative horizontal distance in Grid

    /*Auto*/
    public bool isWaitingFrontRend = false;//Message Buffer -> Renderer
    public bool isWaitingRend = true;//Message Config -> Buffer
    private bool isTheFirst = true;// Special situation: the first char

    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponentInParent<Grid>();
        tilemapSource = GameObject.Find("wordAlphabet").GetComponent<Tilemap>();
        tilemapGoal = GameObject.Find("wordRendererGuide").GetComponent<Tilemap>();
        tilemap = gameObject.GetComponent<Tilemap>();
        inputCharArray = inputStr.ToCharArray();// inputStr(string) -> inputCharArray(char[])
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaitingRend)
        {
            Flush();// Clean Buffer
            inputCharArray = inputStr.ToCharArray();
            // inputStr(string) -> inputCharArray(char[])
            isTheFirst = true;
            Rend();// re-Rend Buffer
            isWaitingRend = false;
            isWaitingFrontRend = true;// Two messages
        }
    }
    private void Flush()
    {
        // <Matrix>
        for (int i = windowLeftTop.x - 1; i <= windowRightTop.x + 8; i++)
        {
            for (int j = windowLeftBottom.y - 50; j <= windowLeftTop.y + 50; j++)
            {
                // </Matrix>
                tilemap.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
    }

    private void Rend()
    {
        Vector3Int nextRendPos = new Vector3Int(0, 0, 0);
        Vector3Int sourceLeftBottom;
        foreach (var letter in inputCharArray)// Rend one char per time
        {
            nextRendPos = RendPointer(nextRendPos, letter);
            sourceLeftBottom = CharToCellPos(letter);
            for (int i = 0; i <= 6; i++)
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
        if (letter == '\n')
            return new Vector3Int(windowLeftTop.x, lastRendPos.y - 9, lastRendPos.z);
        // Special situation: \n
        if (letter == ' ') space = new Vector3Int(2, 0, 0);
        // Special situation: ' '
        if (isTheFirst)
        {
            isTheFirst = false;
            return windowLeftTop;
        }
        // Special situation: the first char
        else
        {
            for (int i = 0; i <= 7; i++)
            {
                for (int j = -2; j <= 5; j++)
                {
                    if (tilemap.GetTile(lastRendPos + new Vector3Int(i, j, 0)) == null) blankRate++;
                }// Find the next rend-able position
                if (blankRate == 8 && !isModify)
                {
                    appliableXValue = lastRendPos.x + i + 1;
                    if (appliableXValue > 298)//Change Line
                    {
                        appliableXValue = windowLeftTop.x;
                        appliableYValue = lastRendPos.y - 9;
                        space = new Vector3Int(0, 0, 0);//Chage Line so that Clear Space Record
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
        // <Checkout the position of each char in the Alphabet>
        if (letter >= '1' && letter <= '9')
        {
            intervalHorizontal = 5;
            intervalVertical = letter - '1';
            return bigALeftBottom + new Vector3Int(intervalHorizontal * 7, -intervalVertical * 8, 0);
        }
        if (letter >= 'A' && letter <= 'K')
        {
            intervalHorizontal = 0;
            intervalVertical = letter - 'A';
            return bigALeftBottom + new Vector3Int(intervalHorizontal * 7, -intervalVertical * 8, 0);
        }
        else if (letter > 'L' && letter <= 'Z')
        {
            intervalHorizontal = 0;
            intervalVertical = letter - 'L';
            return bigLLeftBottom + new Vector3Int(intervalHorizontal * 7, -intervalVertical * 8, 0);
        }
        else if (letter >= 'a' && letter <= 'k')
        {
            intervalHorizontal = 1;
            intervalVertical = letter - 'a';
            return bigALeftBottom + new Vector3Int(intervalHorizontal * 7, -intervalVertical * 8, 0);
        }
        else if (letter > 'l' && letter <= 'z')
        {
            intervalHorizontal = 1;
            intervalVertical = letter - 'l';
            return bigLLeftBottom + new Vector3Int(intervalHorizontal * 7, -intervalVertical * 8, 0);
        }
        else if (letter == '0')
            return bigALeftBottom + new Vector3Int(5 * 7, -72, 0);
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
        else if (letter == ' ' || letter == '\n')
            return bigALeftBottom + new Vector3Int(7 * 7, -16, 0);
        else if (letter == '_')
            return bigALeftBottom + new Vector3Int(2 * 7, -24, 0);
        else if (letter == '#')
            return bigALeftBottom + new Vector3Int(2 * 7, -32, 0);
        else if (letter == '~')
            return bigALeftBottom + new Vector3Int(2 * 7, -40, 0);
        else if (letter == '<')
            return bigALeftBottom + new Vector3Int(2 * 7, -48, 0);
        else if (letter == '>')
            return bigALeftBottom + new Vector3Int(2 * 7, -56, 0);
        else if (letter == '(')
            return bigALeftBottom + new Vector3Int(2 * 7, -64, 0);
        else if (letter == ')')
            return bigALeftBottom + new Vector3Int(2 * 7, -72, 0);
        else if (letter == '+')
            return bigALeftBottom + new Vector3Int(3 * 7, 0, 0);
        else if (letter == '-')
            return bigALeftBottom + new Vector3Int(3 * 7, -8, 0);
        else if (letter == '*')
            return bigALeftBottom + new Vector3Int(3 * 7, -16, 0);
        else if (letter == '/')
            return bigALeftBottom + new Vector3Int(3 * 7, -24, 0);
        else if (letter == '=')
            return bigALeftBottom + new Vector3Int(3 * 7, -32, 0);
        else if (letter == '!')
            return bigALeftBottom + new Vector3Int(3 * 7, -40, 0);
        else if (letter == '?')
            return bigALeftBottom + new Vector3Int(3 * 7, -48, 0);
        else if (letter == '\"')
            return bigALeftBottom + new Vector3Int(3 * 7, -56, 0);
        else if (letter == '\'')
            return bigALeftBottom + new Vector3Int(3 * 7, -64, 0);
        else if (letter == '%')
            return bigALeftBottom + new Vector3Int(3 * 7, -72, 0);
        else if (letter == '@')
            return bigALeftBottom + new Vector3Int(4 * 7, 0, 0);
        else if (letter == '^')
            return bigALeftBottom + new Vector3Int(4 * 7, -8, 0);
        else if (letter == '[')
            return bigALeftBottom + new Vector3Int(4 * 7, -16, 0);
        else if (letter == ']')
            return bigALeftBottom + new Vector3Int(4 * 7, -24, 0);
        else if (letter == '{')
            return bigALeftBottom + new Vector3Int(4 * 7, -32, 0);
        else if (letter == '}')
            return bigALeftBottom + new Vector3Int(4 * 7, -40, 0);
        else return bigALeftBottom + new Vector3Int(4 * 7, -48, 0);
    }
    // </Checkout the position of each char in the Alphabet>
}
