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
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().inputStr =
            "ONE REAL MAN\n" +
            "The ruler of an ancient kingdom wanted to disprove the statement that the men of his domain were ruled by their wives." +
            "He had all the males in his kingdom brought before him and warned that any man who did not tell the truth would be punished severely.\n" +
            "Then he asked all the men who obeyed their wives\' directions and counsel to step to the left side of the hall. " +
            "All the men did so but one little man who moved to the right.\n" +
            "It's good to see, said the king, that we have one real man in the kingdom. " +
            "Tell these chickenhearted dunces why you alone among them stand on the right side of the hall.\n" +
            "Your Majesty , came the reply in a squealing voice, " +
            "it is because before I left home my wife told me to keep out of crowds.";
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
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().inputStr = "For centuries, " +
            "in the countries of south and Southeast Asia the elephant has been an intimate part of the culture, " +
            "economy and religion, and nowhere more so than in Thailand. Unlike its African cousin, the Asian elephant is easily domesticated." +
            " The rare so-called white elephants have actually lent the authority of kingship to its rulers and until the 1920s the national flag was a white elephant on a red background." +
            "To the early Western visitors the country's romantic name was \"Land of the White Elephant\".\n\n" +
            "Today, however, the story is very different. " +
            "Out of work and out of land, the Thai elephant struggles for survival in a nation that no longer needs it." +
            " The elephant has found itself more or less abandoned by previous owners who have moved on to a different economic world and a westernized society. " +
            "And while the elephant's problems began many years ago, now it rates a very low national priority.\n\n" +
            "How this reversal from national icon to neglected animal came about is a tale of worsening environmental and the changing lives of the Thais themselves. " +
            "According to Richard Lair, Thailand\'s experts on the Asian elephant and author of the report Gone Astray, " +
            "at the turn of the century there may well have beenas many as 100,000 domestic elephants in the country." +
            " In the north of Thailand alone it was estimated that more than 20,000 elephants were employed in transport, " +
            "1,000 of them alone on the road between the cities of Chiang Mai and Chiang Saen. " +
            "This was at a time when 90 per cent of Thailand was still forest-a habitat that not only supported the animals but also made them necessary to carry goods and people. " +
            "Nothing ploughs through dense forest better than a massive but sure-footed elephant.\n\n" +
            "By 1950 the elephant population had dropped to a still substantial 13,397, but today there are probably nomore than 3,800, " +
            "with another 1,350 roaming free in the national parks. But now, Thailand\'s forest coversonly 20 per cent of the land. " +
            "This deforestation is the central point of the elephant\'s difficult situation, " +
            "for it has effectively put the animals out of work. " +
            "This century, as the road network grew, so the elephant\'s role as a beast of burden declined.";
        GameObject.Find("wordBoard").GetComponent<SpriteRenderer>().sortingOrder = wordBoardOn;
        GameObject.Find("wordBoardExitButton").
            GetComponent<SpriteRenderer>().sortingOrder = wordBoardExitButtonOn;
        GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = wordRendererOn;
        isAlert = true;
        GameObject.Find("wordBuffer").GetComponent<WordTranslate>().isWaitingRend = true;
    }
}
