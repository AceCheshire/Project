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
    private int guideBoardOn = 29;
    private bool isFinal = false;
    public bool isGuide = false;

    /*Public Status: isAlert && goalScene*/
    private bool isPreDisplay = false;
    private bool isDisplay = true;
    public bool isAlert = false;
    public int goalScene = -2;
    public ManaAppear mana;
    public StageOneStatus status;
    public Camera cam;
    private int finishRate;

    /*Words*/
    private string homeAlertPassage = "Back to WelcomeScene?";
    private string selectAlertPassage = "Back to SelectScene?";
    private string repeatAlertPassage = "Repeat this Scene?";
    private string loseObstacleAlertPassage = "Nut crash some hard stones...\nYou have used ";
    private string loseFallAlertPassage = "Nut fall into the abyss...\nYou have used ";
    private string loseStopAlertPassage = "This is not the end...\nYou have used ";
    private string[] winAlertPassage = new string[32];
    private string guideAlertPassage = "GUIDE\n\n" +
        "Introduction About How To Play This Game\n\nYou can press \" Tab \" button to " +
        "turn to the main \ntext or page down, and \" LeftCtrl \" button to page \nup.";


    // Start is called before the first frame update
    void Start()
    {
        winAlertPassage[0] = "Statistics     Current     Best     World - Best     ";
        winAlertPassage[1] = "Used Mana     ";
        winAlertPassage[2] = "Used Platform    ";
        winAlertPassage[3] = "Used Time    ";
        winAlertPassage[4] = "Used Runtime ";
        if (PlayerPrefs.GetInt("SET") != 1)
        {
            PlayerPrefs.SetInt("SET", 1);
            PlayerPrefs.SetInt("Mana", 100000);
            PlayerPrefs.SetFloat("Runtimer", 100000f);
            PlayerPrefs.SetInt("Tilecount", 100000);
            PlayerPrefs.SetFloat("Timer", 100000f);
            PlayerPrefs.Save();
        }
        //Debug.Log("LayerController Start!");
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlert == true && !isDisplay && !isFinal && !isGuide)
        {
            OpenAlert();
            isDisplay = true;
        }
        if (isAlert == true && !isDisplay && isFinal && !isGuide)
        {
            OpenFinalAlert();
            isDisplay = true;
        }
        if (isAlert == true && !isDisplay && !isFinal && isGuide)
        {
            OpenGuideAlert();
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

    public void OpenGuideAlert()
    {
        GameObject.Find("guideBoard").
            GetComponent<SpriteRenderer>().sortingOrder = guideBoardOn;
        GameObject.Find("guideBoardRightButton").
            GetComponent<SpriteRenderer>().sortingOrder = rightButtonOn;
        GameObject.Find("wordRenderer").
            GetComponent<TilemapRenderer>().sortingOrder = wordRendererOn;
        GameObject.Find("guideBoardRightButton").
            GetComponent<Collider2D>().enabled = true;
        GameObject.Find("wordRenderer").transform.position =
            new Vector3(cam.transform.position.x - 1.25f, cam.transform.position.y - 0.35f, -0.5f);
        GameObject.Find("wordRenderer").transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    /*For Public Reference*/
    public void OneHomeAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslateOne>().inputStr = homeAlertPassage;
        OneAlertOff();
        isGuide = false;
        isAlert = true;
        goalScene = 0;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void OneAlertOff()
    {
        GameObject.Find("smallWordBoard").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("guideBoard").
           GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("smallWordBoardRightButton").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("guideBoardRightButton").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("smallWordBoardLeftButton").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("smallWordBoardRightButton").
            GetComponent<Collider2D>().enabled = false;
        GameObject.Find("smallWordBoardLeftButton").
            GetComponent<Collider2D>().enabled = false;
        GameObject.Find("guideBoardRightButton").
           GetComponent<Collider2D>().enabled = false;
        GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = -1;
        GameObject.Find("wordRenderer").transform.position =
            new Vector3(cam.transform.position.x + 3, cam.transform.position.y - 2.35f, -0.5f);
        GameObject.Find("wordRenderer").transform.localScale = new Vector3(1, 1, 1);
        isAlert = false;
        isPreDisplay = false;
        isGuide = false;
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
        OneAlertOff();
        isGuide = false;
        isAlert = true;
        goalScene = -1;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void OneGuideAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslateOne>().inputStr = guideAlertPassage;
        OneAlertOff();
        isAlert = true;
        isGuide = true;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void OneRepeatAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslateOne>().inputStr = repeatAlertPassage;
        OneAlertOff();
        isGuide = false;
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
                + "Repeat this stage?";
        //Debug.Log(GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().inputStr);
        OneAlertOff();
        GameObject.Find("wordRenderer").transform.position =
            new Vector3(cam.transform.position.x + 2.25f, cam.transform.position.y - 1.85f, -0.5f);
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
        OneAlertOff();
        GameObject.Find("wordRenderer").transform.position =
            new Vector3(cam.transform.position.x + 2.25f, cam.transform.position.y - 1.85f, -0.5f);
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
        OneAlertOff();
        GameObject.Find("wordRenderer").transform.position =
            new Vector3(cam.transform.position.x + 2.25f, cam.transform.position.y - 1.85f, -0.5f);
        isAlert = true;
        goalScene = 1;
        GameObject.Find("wordBuffer").GetComponent<WordTranslateOne>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void OneWinAlertOn()
    {
        if (mana.totalMana < PlayerPrefs.GetInt("Mana"))
        {
            PlayerPrefs.SetInt("Mana", mana.totalMana);
            PlayerPrefs.Save();
        }
        if (status.posList.Count < PlayerPrefs.GetInt("Tilecount"))
        {
            PlayerPrefs.SetInt("Tilecount", status.posList.Count);
            PlayerPrefs.Save();
        }
        if (status.timer < PlayerPrefs.GetFloat("Timer"))
        {
            PlayerPrefs.SetFloat("Timer", status.timer);
            PlayerPrefs.Save();
        }
        if (status.runTimer < PlayerPrefs.GetFloat("Runtimer"))
        {
            PlayerPrefs.SetFloat("Runtimer", status.runTimer);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("achieve2") != "complete")
        {
            finishRate = PlayerPrefs.GetInt("FinishRate");
            PlayerPrefs.SetString("achieve2", "complete");
            PlayerPrefs.SetInt("FinishRate", finishRate + 20);
        }
        JudgeGrade();
        GameObject.Find("wordBufferFinal").
            GetComponent<WordTranslateFinal>().inputStr = winAlertPassage[0] + "\n" +
            winAlertPassage[1] + mana.totalMana + "     " + PlayerPrefs.GetInt("Mana") + "     " + "        -     \n" +
            winAlertPassage[2] + status.posList.Count + "      " + PlayerPrefs.GetInt("Tilecount") + "     " + "         -     \n" +
            winAlertPassage[3] + status.timer + " sec   " + PlayerPrefs.GetFloat("Timer") + " sec     " + "      -\n" +
            winAlertPassage[4] + status.runTimer + " sec   " + PlayerPrefs.GetFloat("Runtimer") + " sec     " + "     -\n\n" +
            winAlertPassage[5];
        OneAlertOff();
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
            winAlertPassage[5] = "\n                      Real SpellCaster. We honor you.";
            if (PlayerPrefs.GetString("achieve3") != "complete")
            {
                finishRate = PlayerPrefs.GetInt("FinishRate");
                PlayerPrefs.SetString("achieve3", "complete");
                PlayerPrefs.SetInt("FinishRate", finishRate + 60);
            }
        }
        if (mana.totalMana > 1500 && mana.totalMana <= 2000)
        {
            GameObject.Find("finalBoard").GetComponent<Animator>().SetBool("isB", true);
            winAlertPassage[5] = "\n                      Practised Magician. Near the top.";
        }
        if (mana.totalMana > 2000)
        {
            GameObject.Find("finalBoard").GetComponent<Animator>().SetBool("isC", true);
            winAlertPassage[5] = "\n                      Just A Noob. You need more efforts.";
        }
    }
}
