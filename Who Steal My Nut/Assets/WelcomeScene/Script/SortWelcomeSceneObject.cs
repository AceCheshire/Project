using UnityEngine;
using UnityEngine.Tilemaps;

public class SortWelcomeSceneObject : MonoBehaviour
{
    /*Default Setting*/
    private int wordBoardOn = 30;
    private int wordBoardExitButtonOn = 31;
    private int wordRendererOn = 31;
    private int finishrate;

    /*Public Status: isAlert*/
    public bool isAlert = false;
    private bool isPreDisplay = false;
    private bool isDisplay = true;
    public NameAlert nameAlert;

    /*Words*/
    private string readmeAlertPassage = "/*You can turn pages with scrollwheel of mouse*/\n\n" +
        "Thanks for playing this games! \n\nIf you want to start playing this game immediately, " +
        "\nwe do not know if you see bottles over a box out of this alert. " +
        "To start the game, you may click the \nmouth of the upper bottle. " +
        "After a short animation, \nyou will get into a scene to select stages. \n\n" +
        "But do not worry, no matter where you go, you can click the homeButton to went back to this menu. \n\n" +
        "We also change the mouse in the game into an \nanimated one with different animation and \nappearence. " +
        "Once you finish some milestones in the game, you can check the achievement, " +
        "there may record your game finish rate.";
    private string achievementAlertPassage = "First Tile\n\n" +
        "  Set the first tile in the stage  --  incomplete\n\n" +
        "Be A Real Thief\n\n" + "  Finish the stage"
        + " for the first time  --  incomplete\n\n"
        + "Thief Master\n\n"
        + "  Finish the stage at A level  --  incomplete\n\n"
        + "Finish Rate --  0%\n\n";

    // Start is called before the first frame update
    void Start()
    {
        //Set Achievement
        if (PlayerPrefs.GetString("achieve1") != "complete")
        {
            PlayerPrefs.SetString("achieve1", "incomplete");
        }
        if (PlayerPrefs.GetString("achieve2") != "complete")
        {
            PlayerPrefs.SetString("achieve2", "incomplete");
        }
        if (PlayerPrefs.GetString("achieve3") != "complete")
        {
            PlayerPrefs.SetString("achieve3", "incomplete");
        }
        if (PlayerPrefs.GetString("achieve4") != "complete")
        {
            PlayerPrefs.SetString("achieve4", "incomplete");
        }
        if (PlayerPrefs.GetString("achieve5") != "complete")
        {
            PlayerPrefs.SetString("achieve5", "incomplete");
        }
        if (PlayerPrefs.GetString("achieve2") == "complete" && PlayerPrefs.GetString("Stage2") == "complete" && PlayerPrefs.GetString("Stage3") == "complete" && PlayerPrefs.GetString("Stage4") == "complete")
        {
            finishrate = PlayerPrefs.GetInt("FinishRate");
            PlayerPrefs.SetString("achieve4", "complete");
            PlayerPrefs.SetInt("FinishRate", finishrate + 30);
        }
        achievementAlertPassage = "First Tile\n\n" +
        "  Set the first platform in the stage  --  " + PlayerPrefs.GetString("achieve1") + "\n\n" +
        "Be A Real Thief\n\n" + "  Finish the stage"
        + " for the first time  --  " + PlayerPrefs.GetString("achieve2") + "\n\n"
        + "Thief Master\n\n"
        + "  Finish the stage at A level  --  " + PlayerPrefs.GetString("achieve3") + "\n\n"
        + "Finish all the stage  --  " + PlayerPrefs.GetString("achieve4") + "\n\n"
        + "Become the best SpellCaster in the world -- " + PlayerPrefs.GetString("achieve5") + "\n\n"
        + "Finish Rate --  " + PlayerPrefs.GetInt("FinishRate") + "%\n\n";
        Debug.Log("LayerController Start!");
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlert == true && !isDisplay && !nameAlert.alertStatus)
        {
            OpenAlert();
            isDisplay = true;
        }
        if (isAlert == true && !isPreDisplay && !nameAlert.alertStatus)
        {
            isDisplay = false;
            isPreDisplay = true;
        }
    }

    public void OpenAlert()
    {
        GameObject.Find("wordBoard").GetComponent<SpriteRenderer>().sortingOrder = wordBoardOn;
        GameObject.Find("wordBoardExitButton").
            GetComponent<SpriteRenderer>().sortingOrder = wordBoardExitButtonOn;
        GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = wordRendererOn;
    }

    /*For Public Reference*/
    public void WelcomeReadmeAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslate>().inputStr = readmeAlertPassage;
        WelcomeAlertOff();
        isAlert = true;
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().isWaitingRend = true;
    }

    /*For Public Reference*/
    public void WelcomeAlertOff()
    {
        GameObject.Find("wordBoard").GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("wordBoardExitButton").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = -1;
        isAlert = false;
        isPreDisplay = false;
    }

    /*For Public Reference*/
    public void WelcomeAchievementAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslate>().inputStr = achievementAlertPassage;
        WelcomeAlertOff();
        isAlert = true;
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().isWaitingRend = true;
    }
}
