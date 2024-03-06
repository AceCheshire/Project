using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SortStageOneObject : MonoBehaviour
{
    /*Default Setting*/
    private int smallWordBoardOn = 29;
    private int rightButtonOn = 30;
    private int leftButtonOn = 30;
    private int wordRendererOn = 31;
    private int finalBoardOn = 29;
    private bool isFinal = false;

    /*Public Status: isAlert && goalScene*/
    private bool isPreDisplay = false;
    private bool isDisplay = true;
    public bool isAlert = false;
    public int goalScene = -2;
    public ManaAppear mana;
    public StageOneStatus status;

    /*Words*/
    private string homeAlertPassage ="Back to WelcomeScene?";
    private string selectAlertPassage = "Back to SelectScene?";
    private string repeatAlertPassage = "Repeat this Scene?";
    private string loseObstacleAlertPassage = "Nut crash some hard stones...\nYou have used ";
    private string loseFallAlertPassage = "Nut fall into the abyss...\nYou have used ";
    private string loseStopAlertPassage = "This is not the end...\nYou have used ";
    private string[] winAlertPassage = new string[32];


    // Start is called before the first frame update
    void Start()
    {
        winAlertPassage[0] = "Statistics     Current     Best     World - Best     ";
        winAlertPassage[1] = "Used Mana     ";
        winAlertPassage[2] = "Used Platform     ";
        winAlertPassage[3] = "Used Time    ";
        winAlertPassage[4] = "Used Runtime ";
        //Debug.Log("LayerController Start!");
    }

    // Update is called once per frame
    void Update()
    {        
        if (isAlert == true && !isDisplay&&!isFinal)
        {
            OpenAlert();
            isDisplay = true;
        }
        if (isAlert == true && !isDisplay && isFinal)
        {
            OpenFinalAlert();
            isDisplay = true;
        }
        if (isAlert == true && !isPreDisplay)
        {
            isDisplay = false;
            isPreDisplay = true;
        }
    }

    public void OpenAlert()
    {
        GameObject.Find("smallWordBoard").
            GetComponent<SpriteRenderer>().sortingOrder = smallWordBoardOn;
        GameObject.Find("smallWordBoardRightButton").
            GetComponent<SpriteRenderer>().sortingOrder = rightButtonOn;
        GameObject.Find("smallWordBoardLeftButton").
            GetComponent<SpriteRenderer>().sortingOrder = leftButtonOn;
        GameObject.Find("wordRenderer").
            GetComponent<TilemapRenderer>().sortingOrder = wordRendererOn;
        GameObject.Find("smallWordBoardRightButton").
            GetComponent<Collider2D>().enabled = true;
        GameObject.Find("smallWordBoardLeftButton").
            GetComponent<Collider2D>().enabled = true;
    }

    public void OpenFinalAlert()
    {
        GameObject.Find("finalBoard").
            GetComponent<SpriteRenderer>().sortingOrder = finalBoardOn;
        GameObject.Find("finalBoardRightButton").
            GetComponent<SpriteRenderer>().sortingOrder = rightButtonOn;
        GameObject.Find("finalBoardLeftButton").
            GetComponent<SpriteRenderer>().sortingOrder = leftButtonOn;
        GameObject.Find("wordRendererFinal").
            GetComponent<TilemapRenderer>().sortingOrder = wordRendererOn;
        GameObject.Find("finalBoardRightButton").
            GetComponent<Collider2D>().enabled = true;
        GameObject.Find("finalBoardLeftButton").
            GetComponent<Collider2D>().enabled = true;
    }

    /*For Public Reference*/
    public void OneHomeAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslateOne>().inputStr = homeAlertPassage;
        isAlert = true;
        goalScene = 0;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void OneAlertOff()
    {
        GameObject.Find("smallWordBoard").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("smallWordBoardRightButton").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("smallWordBoardLeftButton").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("smallWordBoardRightButton").
            GetComponent<Collider2D>().enabled = false;
        GameObject.Find("smallWordBoardLeftButton").
            GetComponent<Collider2D>().enabled = false;
        GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = -1;
        isAlert = false;
        isPreDisplay = false;
    }

    public void OneFinalAlertOff()
    {
        GameObject.Find("finalBoard").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("finalBoardRightButton").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("finalBoardLeftButton").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("finalBoardRightButton").
            GetComponent<Collider2D>().enabled = false;
        GameObject.Find("finalBoardLeftButton").
            GetComponent<Collider2D>().enabled = false;
        GameObject.Find("wordRendererFinal").GetComponent<TilemapRenderer>().sortingOrder = -1;
        isAlert = false;
        isPreDisplay = false;
        isFinal = false;
    }

    /*For Public Reference*/
    public void OneSelectAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslateOne>().inputStr = selectAlertPassage;
        isAlert = true;
        goalScene = -1;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void OneRepeatAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslateOne>().inputStr = repeatAlertPassage;
        isAlert = true;
        goalScene = 1;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void OneLoseObstacleAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslateOne>().inputStr = loseObstacleAlertPassage
                + Mathf.Floor(GameObject.Find("gameStatusConfig").GetComponent<StageOneStatus>().timer) + "s\n"
                +"Repeat this stage?";
        Debug.Log(GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().inputStr);
        isAlert = true;
        goalScene = 1;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void OneLoseFallAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslateOne>().inputStr = loseFallAlertPassage
                + Mathf.Floor(GameObject.Find("gameStatusConfig").GetComponent<StageOneStatus>().timer) + "s\n"
                + "Repeat this stage?";
        isAlert = true;
        goalScene = 1;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For public reference*/
    public void OneLoseStopAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslateOne>().inputStr = loseStopAlertPassage
                + Mathf.Floor(GameObject.Find("gameStatusConfig").GetComponent<StageOneStatus>().timer) + "s\n"
                + "Repeat this stage?";
        isAlert = true;
        goalScene = 1;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void OneWinAlertOn()
    {
        JudgeGrade();
        GameObject.Find("wordBufferFinal").
            GetComponent<WordTranslateFinal>().inputStr = winAlertPassage[0] +"\n"+
            winAlertPassage[1] + mana.totalMana + "        -     " + "        -     \n" +
            winAlertPassage[2] + status.posList.Count + "               -     " + "     -     \n" +
            winAlertPassage[3] + status.timer + " sec           -     " + "     -     \n" +
            winAlertPassage[4] + status.runTimer + " sec          -     " + "     -     \n\n" +
            winAlertPassage[5];
        isAlert = true;
        isFinal = true;
        goalScene = 0;
        GameObject.Find("wordBufferFinal").GetComponent<WordTranslateFinal>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void JumpScene()
    {
        if (goalScene == -1) SceneManager.LoadScene("SelectScene");
        if (goalScene == 0) SceneManager.LoadScene("Menu(WelcomeScene)");
        if (goalScene == 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void JudgeGrade()
    {
        if (mana.totalMana <= 1500)
        {
            GameObject.Find("finalBoard").GetComponent<Animator>().SetBool("isA", true);
            winAlertPassage[5] = "                     Real SpellCaster. We honor you.";
        }
        if (mana.totalMana > 1500 && mana.totalMana <= 2000)
        {
            GameObject.Find("finalBoard").GetComponent<Animator>().SetBool("isB", true);
            winAlertPassage[5] = "                     Practised Magician. Near the top.";
        }
        if (mana.totalMana > 2000)
        {
            GameObject.Find("finalBoard").GetComponent<Animator>().SetBool("isC", true);
            winAlertPassage[5] = "                     Just A Noob. You need more efforts.";
        }
    }
}
