using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseEventSelectButton : MonoBehaviour
{
    /*Static*/
    private float shakeRate = 2f;
    private GameObject boxSet;

    /*Auto*/
    private float timer = 0f;
    public bool isTriggering = false;// If guideSelectButton is triggering
    private bool isCollided = false;// If nut has collided
    private bool isCollidedTwice = false;// If nut has collided twice

    // Start is called before the first frame update
    void Start()
    {
        boxSet = GameObject.Find("backGroundBox");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>1.5f)
        {
            if (isTriggering) ViolentShake();// Mouse Event
            // else SlightShake();
        }
        // Set 1.5f sec because it is the time backGroundBox family fully appear
        if (gameObject.GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0) && isCollidedTwice)
            SceneManager.LoadScene("SelectScene");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "backGroundBox" && isCollided)
        {
            gameObject.GetComponent<AudioSource>().Play();
            isCollidedTwice = true;
        }
        // trigger isCollidedTwice
        if (collision.collider.name == "backGroundBox" && !isCollided)
        {
            isCollided = true;
            gameObject.GetComponent<AudioSource>().Play();
        }// trigger isCollided
    }

    private void SlightShake()
    {
        if (Mathf.Floor(timer) % 2 == 0)// Attention
        {
            // <1/4 sec && 3/4 sec>
            if (timer - Mathf.Floor(timer) < 0.25f ||
                (timer - Mathf.Floor(timer) > 0.5f && timer - Mathf.Floor(timer) < 0.75f))
            {
                boxSet.transform.rotation = new Quaternion
                (boxSet.transform.rotation.x, boxSet.transform.rotation.y,
                 boxSet.transform.rotation.z - Time.deltaTime * 0.01f, boxSet.transform.rotation.w);
            }
            // </1/4 sec && 3/4 sec>
            // <2/4 sec && 4/4 sec>
            if (timer - Mathf.Floor(timer) > 0.75f ||
                (timer - Mathf.Floor(timer) > 0.25f && timer - Mathf.Floor(timer) < 0.5f))
            {
                boxSet.transform.rotation = new Quaternion
                (boxSet.transform.rotation.x, boxSet.transform.rotation.y,
                 boxSet.transform.rotation.z + Time.deltaTime * 0.01f, boxSet.transform.rotation.w);
            }
            // </2/4 sec && 4/4 sec>
        }
    }

    private  void ViolentShake()// The only difference from SlightShake() is shakeRate
    {
        if (Mathf.Floor(timer) % 2 == 0)
        {
            // <1/4 sec && 3/4 sec>
            if (timer - Mathf.Floor(timer) < 0.25f ||
                (timer - Mathf.Floor(timer) > 0.5f && timer - Mathf.Floor(timer) < 0.75f))
            {
                boxSet.transform.rotation = new Quaternion
                (boxSet.transform.rotation.x, 
                 boxSet.transform.rotation.y,
                 boxSet.transform.rotation.z - Time.deltaTime * 0.01f * shakeRate, 
                 boxSet.transform.rotation.w);
            }
            // </1/4 sec && 3/4 sec>
            // <2/4 sec && 4/4 sec>
            if (timer - Mathf.Floor(timer) > 0.75f ||
                (timer - Mathf.Floor(timer) > 0.25f && timer - Mathf.Floor(timer) < 0.5f))
            {
                boxSet.transform.rotation = new Quaternion
                (boxSet.transform.rotation.x,
                 boxSet.transform.rotation.y,
                 boxSet.transform.rotation.z + Time.deltaTime * 0.01f * shakeRate,
                 boxSet.transform.rotation.w);
            }
            // </2/4 sec && 4/4 sec>
        }
    }
}