using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundShaking : MonoBehaviour
{
    /*Time*/
    private float timer = 0f;
    private float streamingSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 0.002f * streamingSpeed;
        if (timer > 0.25f) Shake();
        // Set 0.25f sec because it is the time backGroundBox family almost appear ( streamingSpeed )
    }

    private void Shake()
    {
        if (Mathf.Floor(timer) % 4 == 0)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3
                (0.002f * 0.002f * 1e2f, 0, 0);
        }
        if (Mathf.Floor(timer) % 4 == 1)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3
                (-0.002f * 0.2f, 0, 0);
        }
        if (Mathf.Floor(timer) % 4 == 2)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3
                (0.002f * 0.2f, 0, 0);
        }
        if (Mathf.Floor(timer) % 4 == 3)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3
                (0.002f * 0.002f * -1e2f, 0, 0);
        }
    }
}
