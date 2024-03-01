using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WordStayTwinkle : MonoBehaviour
{
    /*Static*/
    private float rValue;// Color.r
    private float gValue;// Color.g
    private float bValue;// Color.b
    private float appearTime = 1.2f;
    private float disappearTime = 2.3f;

    /*Auto*/
    private float timer = 0f;
    private bool canAppear = true;
    private bool canDisappear = false;

    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (canAppear) Appear();
        if (canDisappear) Disappear();
    }

    private void Appear()
    {
        rValue = gameObject.GetComponent<TilemapRenderer>().material.color.r;
        gValue = gameObject.GetComponent<TilemapRenderer>().material.color.g;
        bValue = gameObject.GetComponent<TilemapRenderer>().material.color.b;
        if (timer > 0.1f)
        {
            gameObject.GetComponent<TilemapRenderer>().material.color = new Color
            (rValue, gValue, bValue,
            (Mathf.PingPong((timer - 0.1f) / (appearTime - 0.1f), 1)) * 0.2f);
            // Appear
        }
        if (timer < 0.1f)
        {
            gameObject.GetComponent<TilemapRenderer>().material.color = new Color
               (rValue, gValue, bValue, 0);// Keep Alpha = 0
        }
        if (timer >= appearTime)
        {
            timer = 0f;
            canAppear = false;
            canDisappear = true;
        }
    }

    private void Disappear()
    {
        rValue = gameObject.GetComponent<TilemapRenderer>().material.color.r;
        gValue = gameObject.GetComponent<TilemapRenderer>().material.color.g;
        bValue = gameObject.GetComponent<TilemapRenderer>().material.color.b;
        if (timer > 1.2f)
        {
            gameObject.GetComponent<TilemapRenderer>().material.color = new Color
            (rValue, gValue, bValue,
            (1 - Mathf.PingPong((timer - 1.2f) / (disappearTime - 1.2f), 1)) * 0.2f);
            // Disappear
        }
        if (timer < 1.2f)
        {
            gameObject.GetComponent<TilemapRenderer>().material.color = new Color
               (rValue, gValue, bValue, 0.2f);// Keep Alpha = 0.2f
        }
        if (timer >= disappearTime)
        {
            timer = 0f;
            canAppear = true;
            canDisappear = false;
        }
    }
}
