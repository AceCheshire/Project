using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Unity.Burst.Intrinsics.X86;

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
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().inputStr =
            "A lady\'s imagination is very rapid; " +
            "it jumps from admiration to love, from love to matrimony in a moment.\n"
            + "An engaged woman is always more agreeable than a disengaged. \n" +
            "She is satisfied with herself.Her cares are over, " +
            "and she feels that she may exert all her powers of pleasing without suspicion.\n"
            + "To see a young couple loving each other is no wonder:\n" +
            "but to see an old couple loving each other is the best sight of all.\n" +
            "Some minds seem almost to create themselves, "
            +"springing up under every disadvantage and working their solitary but irresistible way through a thousand obstacles.";
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
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().inputStr =
            "Happiness in this world, when it comes, comes incidentally. " +
            "Make it the object of pursuit, and it leads us a wild-goose chase, and is never attained.\n" +
            "All sorts of persons, and every individual, has a place to fill in the world, " +
            "and is important in some respects, whether he chooses to be so or not.";
        GameObject.Find("wordBoard").GetComponent<SpriteRenderer>().sortingOrder = wordBoardOn;
        GameObject.Find("wordBoardExitButton").
            GetComponent<SpriteRenderer>().sortingOrder = wordBoardExitButtonOn;
        GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = wordRendererOn;
        isAlert = true;
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().isWaitingRend = true;
    }
}
