using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Data.Common;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
[System.Serializable]
public class Score
{
    public bool success;
    public Data data;
}
[System.Serializable]
public class Data
{
    public string name;
    public int worldmana;
    public int worldplatform;
    public float worldtime;
    public float worldruntime;
}
public class SortStageOneObject : MonoBehaviour
{
    /*Default Setting*/
    UnityWebRequest webRequest;
    private string downloadUrl = "";
    private string uploadUrl = "";
    private string playername = "";
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
    public Score topScore;
    private int finishRate;
    private float time;
    private float runtime;

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
        downloadUrl = "http://62.234.211.190:51638/top/Stage1";
        winAlertPassage[0] = "Statistics     Current     Best     World - Best     ";
        winAlertPassage[1] = "Used Mana     ";
        winAlertPassage[2] = "Used Platform    ";
        winAlertPassage[3] = "Used Time    ";
        winAlertPassage[4] = "Used Runtime ";
        if (!PlayerPrefs.HasKey("Stage1Mana"))
        {
            PlayerPrefs.SetInt("Stage1Mana", 100000);
            PlayerPrefs.SetFloat("Stage1Runtimer", 100000f);
            PlayerPrefs.SetInt("Stage1Tilecount", 100000);
            PlayerPrefs.SetFloat("Stage1Timer", 100000f);
            PlayerPrefs.Save();
        }
        playername = PlayerPrefs.GetString("playername");
        //pull data from web
        StartCoroutine(Down(downloadUrl));
        //pull data
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
    IEnumerator Down(string downloadingUrl)
    {
        webRequest = UnityWebRequest.Get(downloadingUrl);
        webRequest.timeout = 30;
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Download Error:" + webRequest.error);
        }
        else
        {
            Debug.Log("Download content:" + webRequest.downloadHandler.text);
            topScore = JsonUtility.FromJson<Score>(webRequest.downloadHandler.text);
            Debug.Log("Download content:" + topScore.data.worldmana + " ");
        }
    }
    IEnumerator Up(string name, int wmana, int wplat, float wtime, float wrun)
    {
        uploadUrl = "http://62.234.211.190:51638/upload/Stage1/data={\"name\":" + "\"" + name + "\"," + "\"worldmana\":" + wmana + "," + "\"worldplatform\":" + wplat + "," + "\"worldtime\":" + wtime + "," + "\"worldruntime\":" + wrun + "}";
        webRequest = UnityWebRequest.Get(uploadUrl);
        webRequest.timeout = 30;
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("upload Error:" + webRequest.error);
        }
        else
        {
            Debug.Log("upload success:" + uploadUrl);
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
        time = MathF.Round(status.timer, 2);
        runtime = MathF.Round(status.runTimer, 2);
        //send to web
        StartCoroutine(Up(playername, mana.totalMana, status.posList.Count, time, runtime));
        //send to web
        //local judge
        if (mana.totalMana < PlayerPrefs.GetInt("Stage1Mana"))
        {
            PlayerPrefs.SetInt("Stage1Mana", mana.totalMana);
            PlayerPrefs.Save();
        }
        if (status.posList.Count < PlayerPrefs.GetInt("Stage1Tilecount"))
        {
            PlayerPrefs.SetInt("Stage1Tilecount", status.posList.Count);
            PlayerPrefs.Save();
        }
        if (time < PlayerPrefs.GetFloat("Stage1Timer"))
        {
            PlayerPrefs.SetFloat("Stage1Timer", time);
            PlayerPrefs.Save();
        }
        if (runtime < PlayerPrefs.GetFloat("Stage1Runtimer"))
        {
            PlayerPrefs.SetFloat("Stage1Runtimer", runtime);
            PlayerPrefs.Save();
        }
        //local judge
        //world judge
        if (PlayerPrefs.GetInt("Stage1Mana") < topScore.data.worldmana && PlayerPrefs.GetInt("Stage1Tilecount") < topScore.data.worldplatform && PlayerPrefs.GetFloat("Stage1Timer") < topScore.data.worldtime && PlayerPrefs.GetFloat("Stage1Runtimer") < topScore.data.worldruntime)
        {
            topScore.data.name = playername;
            topScore.data.worldmana = PlayerPrefs.GetInt("Stage1Mana");
            topScore.data.worldplatform = PlayerPrefs.GetInt("Stage1Tilecount");
            topScore.data.worldruntime = PlayerPrefs.GetFloat("Stage1Runtimer");
            topScore.data.worldtime = PlayerPrefs.GetFloat("Stage1Timer");
            PlayerPrefs.SetString("achieve5", "complete");
        }
        //world judge
        if (PlayerPrefs.GetString("achieve2") != "complete")
        {
            finishRate = PlayerPrefs.GetInt("FinishRate");
            PlayerPrefs.SetString("achieve2", "complete");
            PlayerPrefs.SetInt("FinishRate", finishRate + 20);
        }
        JudgeGrade();
        GameObject.Find("wordBufferFinal").
            GetComponent<WordTranslateFinal>().inputStr = winAlertPassage[0] + topScore.data.name + "\n" +
            winAlertPassage[1] + mana.totalMana + "     " + PlayerPrefs.GetInt("Stage1Mana") + "                 " + topScore.data.worldmana + "     \n" +
            winAlertPassage[2] + status.posList.Count + "      " + PlayerPrefs.GetInt("Stage1Tilecount") + "               " + topScore.data.worldplatform + "              \n" +
            winAlertPassage[3] + time + " sec   " + PlayerPrefs.GetFloat("Stage1Timer") + " sec       " + topScore.data.worldtime + "  sec    " + "\n" +
            winAlertPassage[4] + runtime + " sec   " + PlayerPrefs.GetFloat("Stage1Runtimer") + " sec       " + topScore.data.worldruntime + " sec   \n\n" +
            winAlertPassage[5];
        OneAlertOff();
        isAlert = true;
        isFinal = true;
        goalScene = -1;
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
            winAlertPassage[5] = "\n                      " + PlayerPrefs.GetString("playername") + ", " +
                "\n                      Real SpellCaster. We honor you.";
            if (PlayerPrefs.GetString("achieve3") != "complete")
            {
                finishRate = PlayerPrefs.GetInt("FinishRate");
                PlayerPrefs.SetString("achieve3", "complete");
                PlayerPrefs.SetInt("FinishRate", finishRate + 30);
            }
        }
        if (mana.totalMana > 1500 && mana.totalMana <= 2000)
        {
            GameObject.Find("finalBoard").GetComponent<Animator>().SetBool("isB", true);
            winAlertPassage[5] = "\n                      " + PlayerPrefs.GetString("playername") + ", " +
                "\n                      Practised Magician. Near the top.";
        }
        if (mana.totalMana > 2000)
        {
            GameObject.Find("finalBoard").GetComponent<Animator>().SetBool("isC", true);
            winAlertPassage[5] = "\n                      " + PlayerPrefs.GetString("playername") + ", " +
                "\n                      Just A Noob. You need more efforts.";
        }
    }
}
