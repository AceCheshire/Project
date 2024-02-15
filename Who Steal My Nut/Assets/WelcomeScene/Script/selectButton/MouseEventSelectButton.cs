using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseEventSelectButton : MonoBehaviour
{
    /*Static*/
    private float shakeRate = 3f;
    private float appearTime = 1.2f;
    private float rValue;// Color.r
    private float gValue;// Color.g
    private float bValue;// Color.b

    /*Auto*/
    private float timer = 0f;
    private bool isTriggering = false;// If mouse is triggering
    private bool isAlert = false;// reference of SortWelcomeSceneObject.isAlert
    private bool isCollided = false;// If nut has collided
    private bool isCollidedTwice = false;// If nut has collided twice

    // Start is called before the first frame update
    void Start()
    {
        isAlert = GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().isAlert;// Avoid error when LoadScene
        Debug.Log("selectButton Start!");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Appear();// Appear Effect
        isAlert = GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().isAlert;// Avoid error when LoadScene
        if(timer>1.5f)
        {
            if (isTriggering) ViolentShake();// Mouse Event
            //else SlightShake();
        }
        // Set 1.5f sec because it is the time backGroundBox family fully appear
        if (gameObject.GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0) && isCollidedTwice)
            SceneManager.LoadScene("StageChoose");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "backGroundBox" && isCollided) isCollidedTwice = true;
        // trigger isCollidedTwice
        if (collision.collider.name == "backGroundBox") isCollided = true;// trigger isCollided
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // <ShakeIntensity Slight->Violent>
        if (collision.name == "mouse" && isAlert == false)
        {
            isTriggering = true;
        }
        // </ShakeIntensity Slight->Violent>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Prepare To Open Stagechoose Scene>
        if (collision.name == "mouse" && isAlert == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
        // </Prepare To Open Stagechoose Scene>
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // <Animator Hover->Off>
        if (collision.name == "mouse" && isAlert == false)
        {
            isTriggering = false;
        }
        // </Animator Hover->Off>
    }

    private void SlightShake()
    {
        if (Mathf.Floor(timer) % 2 == 0)// Attention
        {
            // <1/4 sec && 3/4 sec>
            if (timer - Mathf.Floor(timer) < 0.25f ||
                (timer - Mathf.Floor(timer) > 0.5f && timer - Mathf.Floor(timer) < 0.75f))
            {
                gameObject.transform.rotation = new Quaternion
                (gameObject.transform.rotation.x, gameObject.transform.rotation.y,
                 gameObject.transform.rotation.z - Time.deltaTime * 0.02f, gameObject.transform.rotation.w);
            }
            // </1/4 sec && 3/4 sec>
            // <2/4 sec && 4/4 sec>
            if (timer - Mathf.Floor(timer) > 0.75f ||
                (timer - Mathf.Floor(timer) > 0.25f && timer - Mathf.Floor(timer) < 0.5f))
            {
                gameObject.transform.rotation = new Quaternion
                (gameObject.transform.rotation.x, gameObject.transform.rotation.y,
                 gameObject.transform.rotation.z + Time.deltaTime * 0.02f, gameObject.transform.rotation.w);
            }
            // </2/4 sec && 4/4 sec>
        }
    }

    private  void ViolentShake()// The only difference from SlightShake() is shakeRate
    {
        if (Mathf.Floor(timer) % 2 == 0)// Attention
        {
            // <1/4 sec && 3/4 sec>
            if (timer - Mathf.Floor(timer) < 0.25f ||
                (timer - Mathf.Floor(timer) > 0.5f && timer - Mathf.Floor(timer) < 0.75f))
            {
                gameObject.transform.rotation = new Quaternion
                (gameObject.transform.rotation.x, 
                 gameObject.transform.rotation.y,
                 gameObject.transform.rotation.z - Time.deltaTime * 0.02f * shakeRate, 
                 gameObject.transform.rotation.w);
            }
            // </1/4 sec && 3/4 sec>
            // <2/4 sec && 4/4 sec>
            if (timer - Mathf.Floor(timer) > 0.75f ||
                (timer - Mathf.Floor(timer) > 0.25f && timer - Mathf.Floor(timer) < 0.5f))
            {
                gameObject.transform.rotation = new Quaternion
                (gameObject.transform.rotation.x,
                 gameObject.transform.rotation.y,
                 gameObject.transform.rotation.z + Time.deltaTime * 0.02f * shakeRate,
                 gameObject.transform.rotation.w);
            }
            // </2/4 sec && 4/4 sec>
        }
    }

    private void Appear()
    {
        rValue = gameObject.GetComponent<SpriteRenderer>().material.color.r;
        gValue = gameObject.GetComponent<SpriteRenderer>().material.color.g;
        bValue = gameObject.GetComponent<SpriteRenderer>().material.color.b;
        if (timer > 0.5f && timer < appearTime)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color
                (rValue, gValue, bValue, Mathf.PingPong(timer / appearTime, 1));// Appear
        }
        if (timer < 0.5f)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color
               (rValue, gValue, bValue, 0);// Keep Alpha = 0
        }
    }
}