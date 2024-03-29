using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutAppear : MonoBehaviour
{
    /*Auto Parameter*/
    private float timer = 0f;
    private float appearRate = 0f;

    /*Static Parameter*/
    private Vector3 endPos;
    private Vector3 startPos;
    private float appearTime = 0.9f;// Duration
    private float startTime = 1.1f;// Start point
    private bool isPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        endPos = new Vector3(-1.57f, -3.48f, 0);// Set End Position
        startPos = new Vector3(0, -3.48f, 0);// Set Start Position
        gameObject.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        appearRate = (timer - startTime) / appearTime;// Count these parameters per frame
        if (timer >= startTime )
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;// Appear
            if (!isPlayed)
            {
                gameObject.GetComponent<AudioSource>().Play();
                isPlayed = true;
            }
        }
        if (timer >= startTime && timer <= startTime + appearTime)
        {
            gameObject.transform.position = appearRate * endPos + (1 - appearRate) * startPos;// Move
        }
    }
}
