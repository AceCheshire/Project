using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTiltleShake : MonoBehaviour
{
    /*Time*/
    private float timer = 0f;

    /*Default Setting*/
    private Vector3 defaultPos = new Vector3(-18.4f, 6f, 0);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = defaultPos;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Shake();
    }

    private void Shake()
    {
        if(Mathf.Floor (timer)%2==1)
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * 0.03f, 0, 0);
        }
        if(Mathf.Floor(timer) % 2 == 0)
        {
            gameObject.transform.position += new Vector3(-Time.deltaTime * 0.03f, 0, 0);
        }
        // Horizontal Move
        if (Mathf.Floor(timer) % 3 == 0)
        {
            gameObject.transform.position += new Vector3(0, Time.deltaTime * 0.015f, 0);
        }
        if (Mathf.Floor(timer) % 3 == 1)
        {
            gameObject.transform.position += new Vector3(0, -Time.deltaTime * 0.015f, 0);
        }
        if (Mathf.Floor(timer) % 3 == 2)
        {
            gameObject.transform.position = gameObject.transform.position;
        }
        // Vertical Move
    }
}
