using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;
using static UnityEngine.ParticleSystem;
using UnityEngine.Timeline;

public class SortWelcomeSceneObject : MonoBehaviour
{
    /*Default Setting*/
    private int wordBoardOn = 30;
    private int wordBoardExitButtonOn = 31;
    private int wordRendererOn = 31;

    /*Public Status: isAlert*/
    public bool isAlert = false;

    /*Words*/
    private string readmeAlertPassage = "/*You can turn pages with scrollwheel of mouse*/\n\n" +
        "Thanks for playing this games! \n\nIf you want to start playing this game immediately, " +
        "we do not know if you see bottles over a box out of this alert. " +
        "To start the game, you may click the mouth of the upper bottle. " +
        "After a short animation, you will get into a scene to select stages. \n\n" +
        "But do not worry, no matter where you go, you can click the home button to went back to this menu. \n\n" +
        "We also change the mouse in the game into a dynamic one with different animation and appearence. " +
        "Once you finish some milestones in the game, you can check the achievement, " +
        "there may record your game finish rate.";
    private string achievementAlertPassage = "First Tile\n\n" +
        "  Set the first tile in the stage  --  incomplete\n\n"+
        "Be A Real Thief\n\n"+"  Finish the stage"
        +" for the first time  --  incomplete\n\n"
        +"Thief Master\n\n"
        +"  Finish the stage at A level  --  incomplete\n\n"
        +"Finish Rate --  0%\n\n";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LayerController Start!");
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    /*For Public Reference*/
    public void WelcomeReadmeAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslate>().inputStr = readmeAlertPassage;
        GameObject.Find("wordBoard").GetComponent<SpriteRenderer>().sortingOrder = wordBoardOn;
        GameObject.Find("wordBoardExitButton").
            GetComponent<SpriteRenderer>().sortingOrder = wordBoardExitButtonOn;
        GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = wordRendererOn;
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
    }

    /*For Public Reference*/
    public void WelcomeAchievementAlertOn()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslate>().inputStr = achievementAlertPassage;
        GameObject.Find("wordBoard").GetComponent<SpriteRenderer>().sortingOrder = wordBoardOn;
        GameObject.Find("wordBoardExitButton").
            GetComponent<SpriteRenderer>().sortingOrder = wordBoardExitButtonOn;
        GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = wordRendererOn;
        isAlert = true;
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().isWaitingRend = true;
    }
}
