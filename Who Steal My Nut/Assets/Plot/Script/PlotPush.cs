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
    private int nowPage = 4;

    // Start is called before the first frame update
    void Start()
    {
        wordRenderer = GameObject.Find("wordRenderer");
        plot[0] = "Welcome to plot scene!";
        plot[1] = "Can you see these lines?";
        plot[2] = "Well, we now need to tell you a story...";
        plot[3] = "Ready to turn to StageOne >>> ";
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
        if (canDisappear && isClick) Disappear();  
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
