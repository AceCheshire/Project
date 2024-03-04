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
    private int nowPage = 5;

    // Start is called before the first frame update
    void Start()
    {
        wordRenderer = GameObject.Find("wordRenderer");
        plot[0] = "Kidd  Well, we have got into Ratatoskr's personal  storage space, right, Captain Retina? \n\n" +
            "Retina  Yeah, Kidd, we can now think it true.\n\n" +
            "/*Ratatoskr is a legendary creation living in the world tree Yggdrasill." +
            " Apperently, we are to steal that legendary fruit. " +
            "That is also what Ratatoskr eat  --  nut that only grows in the Yggdrasill. " +
            "As the most famous party all around this Kingdom, we do succesfully get that rare nut. " +
            "However, due to Retina's miscalculation, we have been captured by Ratatoskr's space magic \" Magic Room \" ," +
            " as well as nut we newly  get. */";
        plot[1] = "Kidd  Too terrible, I cannot stay in this plain and monotonous place a minute more!\n\n" +
            "You  Theoretically, Ratatoskr's magic cannot be perfect and flawless. " +
            "There may be some weak points so that we can set teleport magic circle there. " +
            "Let us find somewhere like these ones.\n\n" +
            "Kidd  Are you kidding? That is magic by Ratatoskr, we all know its splendid magic skill, " +
            "and you tell me this room may leak water?\n\n" +
            "Retina  That is reckless, Kidd. " +
            "Nobody tells you that Ratatoskr or every splendid spellcaster will not create unperfect magic.\n\n" +
            "/*We start to wonder in this magic room. */\n";
        plot[2] = "Kidd  Suck your cock! Hard to commit, but it's true! " +
            "Look, that point may be able to set  magic circle.\n\n" +
            "You  Thanks, Kidd. I set teleport magic circle there now. " +
            "The destination is the magic tower of Kingdom. The next step is to transport the nut to the magic circle.\n\n" +
            "Retina  The problem is, how?\n\nYou I can use earth magic \"Upper - class Earth Platform Create\"," +
            " which can create gallery from the place of nut to the circle.";
        plot[3] = "Kidd Don't you see something dark in this room? " +
            "Those are Ratatoskr's magic crystals and if nut crashes it, the nut cannot be safe and sound.\n\n" +
            "Retina  Well, I can use magic \"Upper - class Enchant\", so I can help, too." +
            "Enchanted platform can have magical power to transport the nut. " +
            "Relatively, it costs a big amount of mana. " +
            "We donnot know if we will meet enemies when we get out, so we had better save our mana.\n\n" +
            "You  Do you see the nut, Kidd? Once I finish my magic, you push the nut down.";
        plot[4] = "Ready to turn to StageOne >>> ";
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
