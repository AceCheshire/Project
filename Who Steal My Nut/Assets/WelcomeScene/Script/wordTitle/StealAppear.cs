using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealAppear : MonoBehaviour
{
    /*Auto Parameter*/
    private float timer = 0f;
    private float appearRate = 0f;

    /*Static Parameter*/
    private Vector3 endPos;
    private Vector3 startPos;
    private float appearTime = 1.2f;// Duration
    private float startTime = 0.4f;// Start point

    // Start is called before the first frame update
    void Start()
    {
        endPos = new Vector3(-1.66f, -4.81f, 0);// Set End Position
        startPos = new Vector3(-1f, -4.81f, 0);// Set Start Position
        gameObject.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        appearRate = (timer - startTime) / appearTime;// Count these parameters per frame
        if (timer >= startTime)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;// Appear
        }
        if (timer >= startTime && timer <= startTime + appearTime)
        {
            gameObject.transform.position = appearRate * endPos + (1 - appearRate) * startPos;// Move
        }
    }
}
