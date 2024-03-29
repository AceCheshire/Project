using UnityEngine;

public class WhoAppear : MonoBehaviour
{
    /*Auto Parameter*/
    private float timer = 0f;
    private float appearRate = 0f; 

    /*Static Parameter*/
    private Vector3 endPos;
    private Vector3 startPos;
    private float appearTime = 1f;// Duration
    private float startTime = 0.1f;// Start point

    // Start is called before the first frame update
    void Start()
    {
        endPos = new Vector3(-4.9f, -3.23f, 0);// Set End Position
        startPos = new Vector3(-4f, -3.23f, 0);// Set Start Position
        gameObject.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        appearRate = (timer - startTime) / appearTime;// Count these parameters per frame
        if (timer >= startTime)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;// Appear
        }
        if (timer >= startTime && timer <= startTime + appearTime)
        {
            gameObject.transform.position = appearRate * endPos + (1 - appearRate) * startPos;// Move
        }
    }
}
