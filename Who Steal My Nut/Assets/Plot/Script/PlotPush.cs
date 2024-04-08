using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlotPush : MonoBehaviour
{
    /*Static*/
    private GameObject wordRenderer;
    private float rValue;// Color.r
    private float gValue;// Color.g
    private float bValue;// Color.b
    private float appearTime = 1.2f;
    private float disappearTime = 0.5f;

    /*Auto*/
    private float timer = 0f;
    private bool canSend = true;
    private bool canAppear = true;
    private bool canDisappear = false;
    private bool isClick = true;

    /*Plot*/
    private string[] plot = new string[32];
    private int pageNumber = 0;
    private int nowPage = 17;
    private Vector3 defaultPlace;

    // Start is called before the first frame update
    void Start()
    {
        wordRenderer = GameObject.Find("wordRenderer");
        plot[0] = "Kidd   \nWell, we have got into Ratatoskr's personal storage space, right, Captain Retina? \n\n";
        plot[1]=  "Retina   \nYeah, Kidd, we can now think it true.\n\n";
        plot[2] = "Ratatoskr is a legendary creation living in the world tree Yggdrasill." +
             " Apperently, we are to steal that \nlegendary fruit. " +
             "That is also what Ratatoskr eat  --  nut that only grows in the Yggdrasill. " +
             "As the most \nfamous party all around this Kingdom, we do \nsuccessfully get that rare nut. " +
             "However, due to \nRetina's miscalculation, we have been captured by \nRatatoskr's space magic \" Magic Room \" ," +
             " as well as nut we newly get. ";
        plot[3] = "Kidd   \nToo terrible, I cannot stay in this plain and \nmonotonous place a minute more!\n\n";
        plot[4] = PlayerPrefs.GetString("playername") + "\nTheoretically, Ratatoskr's magic cannot be \nperfect and flawless. " +
            "There may be some weak \npoints so that we can set teleport magic circle there anyway. " +
            "Let us find somewhere like these ones.\n\n";
        plot[5] = "Kidd   \nAre you kidding? That is magic by Ratatoskr, we all know its splendid magic skill, " +
            "and you tell me \nthis room may leak water?\n\n";
        plot[6] = "Retina   \nThat is reckless, Kidd. " +
            "Nobody tells you that \nRatatoskr or every splendid spellcaster will not \ncreate unperfect magic.\n\n";
        plot[7] = "/*We start to wonder in this magic room. */\n";
        plot[8] = "Kidd   \nSuck your cock! Hard to commit, but it's true! " +
            "Look, that point may be able to set  magic circle.\n\n";
        plot[9] = PlayerPrefs.GetString("playername") + "\nThanks, Kidd. I set teleport magic circle there now. " +
        "\nThe destination is the magic tower of Kingdom. The next step is to transport the nut to the magic circle.\n\n";
        plot[10] = "Retina   \nThe problem is, how?\n\n";
        plot[11] = PlayerPrefs.GetString("playername") + "\nI can use earth magic \"Upper - class Earth \nPlatform Create\"," +
            " which can create gallery from the place of nut to the circle.\n\n";
        plot[12] = "Kidd   \nDon't you see something dark in this room? " +
        "\nThose are Ratatoskr's magic crystals and if nut \ncrashes it, the nut cannot be safe and sound.\n\n";
        plot[13] = "Retina   \nWell, I can use magic \"Upper - class Enchant\", so I can help, too." +
        "Enchanted platform can have magical power to transport the nut. ";
        plot[14] = "Retina   \nRelatively, it costs a big amount of mana. " +
        "We \ndonnot know if we will meet enemies when we \nget out, so we had better save our mana.\n\n";
        plot[15] = PlayerPrefs.GetString("playername") + "\nDo you see the nut, Kidd? Once I finish my \nmagic, you push the nut down.";
        plot[16] = "Ready to turn to StageOne >>> ";
        // Do not display this last line, but to avoid error and should not be deleted
        defaultPlace = GameObject.Find("wordRenderer").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && !isClick)
        {
            timer = 0;
            isClick = true;
            canSend = true;
        }
        if (canAppear && isClick)
        {
            if (canSend) SendRendMessage();
            Appear();// Appear & Disappear Effect
        }
        if (canDisappear) Disappear();  
    }

    private void SendRendMessage()
    {
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslate>().inputStr = plot[pageNumber];// Change words
        GameObject.Find("wordBuffer").
            GetComponent<WordTranslate>().isWaitingRend = true;
        if (pageNumber <= nowPage - 2)
        {
            if (pageNumber == 3)
            {
                GameObject.Find("wordRenderer").transform.position = defaultPlace;
                GameObject.Find("wordLowerBoard").GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
            if (pageNumber == 0 || pageNumber == 3 || pageNumber == 5 || pageNumber == 8 || pageNumber == 12)
            {
                GameObject.Find("Kidd").GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else GameObject.Find("Kidd").GetComponent<SpriteRenderer>().sortingOrder = -1;
            if (pageNumber == 1 || pageNumber == 6 || pageNumber == 10 || pageNumber == 13 || pageNumber == 14)
            {
                GameObject.Find("Retina").GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else GameObject.Find("Retina").GetComponent<SpriteRenderer>().sortingOrder = -1;
            pageNumber++;
            canSend = false;
        }
        else
        {
            canSend = false;
            SceneManager.LoadScene("StageOne(Tutorial)");// Turn to another scene
        }
        if (pageNumber == 3)
        {
            GameObject.Find("wordRenderer").transform.position = new(0, 0, 0);
            GameObject.Find("wordLowerBoard").GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
        }

    private void Appear()
    {
        rValue = wordRenderer.GetComponent<TilemapRenderer>().material.color.r;
        gValue = wordRenderer.GetComponent<TilemapRenderer>().material.color.g;
        bValue = wordRenderer.GetComponent<TilemapRenderer>().material.color.b;
        if (timer > 0.2f && timer < appearTime)
        {
            if (timer > 0.2f && timer < 0.5f)
            {
                GameObject.Find("Retina").transform.position = new(0, -6.7f - 0.3f * Mathf.PingPong((timer - 0.2f) / 0.3f, 1), 0);
                GameObject.Find("Kidd").transform.position = new(0, -6.82f - 0.18f * Mathf.PingPong((timer - 0.2f) / 0.3f, 1), 0);
            }
            wordRenderer.GetComponent<TilemapRenderer>().material.color = new Color
            (rValue, gValue, bValue, Mathf.PingPong(timer / appearTime, 1));// Appear
        }
        if (timer < 0.2f)
        {
            GameObject.Find("Retina").transform.position = new(0, -7 + 0.3f * Mathf.PingPong(timer / 0.2f, 1), 0);
            GameObject.Find("Kidd").transform.position = new(0, -7 + 0.18f * Mathf.PingPong(timer / 0.2f, 1), 0);
            wordRenderer.GetComponent<TilemapRenderer>().material.color = new Color
                (rValue, gValue, bValue, 0);// Keep Alpha = 0
        }
        if (timer >= appearTime)
        {
            canAppear = false;
            canDisappear = true;
            isClick = false;
        }
    }

    private void Disappear()
    {
        rValue = wordRenderer.GetComponent<TilemapRenderer>().material.color.r;
        gValue = wordRenderer.GetComponent<TilemapRenderer>().material.color.g;
        bValue = wordRenderer.GetComponent<TilemapRenderer>().material.color.b;
        if (timer > 0.1f && timer < disappearTime)
        {
            wordRenderer.GetComponent<TilemapRenderer>().material.color = new Color
            (rValue, gValue, bValue, 1 - Mathf.PingPong(timer / disappearTime, 1));// Disappear
        }
        if (timer < 0.1f)
        {
            wordRenderer.GetComponent<TilemapRenderer>().material.color = new Color
               (rValue, gValue, bValue, 1);// Keep Alpha = 1
        }
        if (timer >= disappearTime)
        {
            canAppear = true;
            canDisappear = false;
            isClick = false;
        }
    }
}
