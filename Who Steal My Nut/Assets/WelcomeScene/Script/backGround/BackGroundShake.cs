using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundShaking : MonoBehaviour
{
    /*Time*/
    private float timer = 0f;
    private float streamingSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("backGround Start!");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * streamingSpeed;
        Shake();
    }

    private void Shake()
    {
        if (Mathf.Floor(timer) % 4 == 0)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3
                (Time.deltaTime * Time.deltaTime * 1e2f, 0, 0);
        }
        if (Mathf.Floor(timer) % 4 == 1)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3
                (-Time.deltaTime * 0.2f, 0, 0);
        }
        if (Mathf.Floor(timer) % 4 == 2)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3
                (Time.deltaTime * 0.2f, 0, 0);
        }
        if (Mathf.Floor(timer) % 4 == 3)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3
                (Time.deltaTime * Time.deltaTime * -1e2f, 0, 0);
        }
    }
}
