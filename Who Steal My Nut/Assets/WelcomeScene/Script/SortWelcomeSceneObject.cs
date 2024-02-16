using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SortWelcomeSceneObject : MonoBehaviour
{
    /*Default Setting*/
    private int wordBoardOn = 30;
    private int wordBoardExitButtonOn = 31;
    private int wordRendererOn = 31;

    /*Public Status: isAlert*/
    public bool isAlert = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("layerController Start!");
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    /*For Public Reference*/
    public void WelcomeReadmeAlertOn()
    {
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().inputStr = "This is Readme.";
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
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().inputStr = "This is Achievement.";
        GameObject.Find("wordBoard").GetComponent<SpriteRenderer>().sortingOrder = wordBoardOn;
        GameObject.Find("wordBoardExitButton").
            GetComponent<SpriteRenderer>().sortingOrder = wordBoardExitButtonOn;
        GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = wordRendererOn;
        isAlert = true;
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().isWaitingRend = true;
    }
}
