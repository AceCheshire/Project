using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SortWelcomeSceneObject : MonoBehaviour
{
    /*Default Setting*/
    private int wordBoardOn = 30;
    private int wordBoardExitButtonOn = 31;

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
        GameObject.Find("wordBoard").GetComponent<SpriteRenderer>().sortingOrder = wordBoardOn;
        GameObject.Find("wordBoardExitButton").
            GetComponent<SpriteRenderer>().sortingOrder = wordBoardExitButtonOn;
        isAlert = true;
    }

    /*For Public Reference*/
    public void WelcomeReadmeAlertOff()
    {
        GameObject.Find("wordBoard").GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameObject.Find("wordBoardExitButton").
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        isAlert = false;
    }
}
