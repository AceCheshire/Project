using System.Collections;
using System.Collections.Generic;
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
    private float appearTime = 2f;
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
    private int nowPage = 8;

    // Start is called before the first frame update
    void Start()
    {
        wordRenderer = GameObject.Find("wordRenderer");
        plot[0] = "Kidd   Well, we have got into Ratatoskr's personal    storage space, right, Captain Retina? \n\n" +
            "Retina   Yeah, Kidd, we can now think it true.\n\n";
        plot[1] = "Ratatoskr is a legendary creation living in the world tree Yggdrasill." +
             " Apperently, we are to steal that \nlegendary fruit. " +
             "That is also what Ratatoskr eat  --  nut that only grows in the Yggdrasill. " +
             "As the most \nfamous party all around this Kingdom, we do \nsuccessfully get that rare nut. " +
             "However, due to \nRetina's miscalculation, we have been captured by \nRatatoskr's space magic \" Magic Room \" ," +
             " as well as nut we newly  get. ";
        plot[2] = "Kidd   Too terrible, I cannot stay in this plain and \nmonotonous place a minute more!\n\n" +
            "You   Theoretically, Ratatoskr's magic cannot be \nperfect and flawless. " +
            "There may be some weak \npoints so that we can set teleport magic circle there anyway. " +
            "Let us find somewhere like these ones.\n\n";
        plot[3] = "Kidd   Are you kidding? That is magic by Ratatoskr, we all know its splendid magic skill, " +
            "and you tell me \nthis room may leak water?\n\n" +
            "Retina   That is reckless, Kidd. " +
            "Nobody tells you that Ratatoskr or every splendid spellcaster will not \ncreate unperfect magic.\n\n" +
            "/*We start to wonder in this magic room. */\n";
        plot[4] = "Kidd   Suck your cock! Hard to commit, but it's true! " +
            "Look, that point may be able to set  magic circle.\n\n" +
            "You   Thanks, Kidd. I set teleport magic circle there now. " +
            "The destination is the magic tower of Kingdom. The next step is to transport the nut to the magic \ncircle.\n\n" +
            "Retina   The problem is, how?\n\n";
        plot[5] = "You   I can use earth magic \"Upper - class Earth \nPlatform Create\"," +
            " which can create gallery from the place of nut to the circle.\n\n" +
            "Kidd   Don't you see something dark in this room? " +
            "\nThose are Ratatoskr's magic crystals and if nut \ncrashes it, the nut cannot be safe and sound.\n\n";
        plot[6] =    "Retina   Well, I can use magic \"Upper - class \nEnchant\", so I can help, too." +
            "Enchanted platform can have magical power to transport the nut. " +
            "Relatively, it costs a big amount of mana. " +
            "We donnot know if we will meet enemies when we get out, so we had \nbetter save our mana.\n\n" +
            "You   Do you see the nut, Kidd? Once I finish my \nmagic, you push the nut down.";
        plot[7] = "Ready to turn to StageOne >>> ";
        // Do not display this last line, but to avoid error and should not be deleted
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
            pageNumber++;
            canSend = false;
        }
        else
        {
            canSend = false;
            SceneManager.LoadScene("StageOne(Tutorial)");// Turn to another scene
        }
    }

    private void Appear()
    {
        rValue = wordRenderer.GetComponent<TilemapRenderer>().material.color.r;
        gValue = wordRenderer.GetComponent<TilemapRenderer>().material.color.g;
        bValue = wordRenderer.GetComponent<TilemapRenderer>().material.color.b;
        if (timer > 0.2f && timer < appearTime)
        {
            wordRenderer.GetComponent<TilemapRenderer>().material.color = new Color
            (rValue, gValue, bValue, Mathf.PingPong(timer / appearTime, 1));// Appear
        }
        if (timer < 0.2f)
        {
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
