using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundBoxAppear : MonoBehaviour
{
    /*Static*/
    private Transform[] children;
    private float appearTime = 1.8f;

    /*Auto*/
    private float timer = 0f;
    private GameObject thatObject;// Vessel of GameObjects
    private float rValue;// Color.r
    private float gValue;// Color.g
    private float bValue;// Color.b

    // Start is called before the first frame update
    void Start()
    {
         children = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        foreach (var child in children)
        {
            Appear(child.name);
        }
        Appear(gameObject.name);
    }

    private void Appear(string name)
    {
        thatObject = GameObject.Find(name);
        rValue = thatObject.GetComponent<SpriteRenderer>().material.color.r;
        gValue = thatObject.GetComponent<SpriteRenderer>().material.color.g;
        bValue = thatObject.GetComponent<SpriteRenderer>().material.color.b;
        if (timer > 0.5f && timer < appearTime)
        {
            thatObject.GetComponent<SpriteRenderer>().material.color = new Color
                (rValue, gValue, bValue, Mathf.PingPong(timer / appearTime, 1));// Appear
        }
        if (timer < 0.5f)
        {
            thatObject.GetComponent<SpriteRenderer>().material.color = new Color
               (rValue, gValue, bValue, 0);// Keep Alpha = 0
        }
    }
}
