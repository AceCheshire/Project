using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MouseEventReadmeButton : MonoBehaviour
{
    /*Static*/
    private float rValue;// Color.r
    private float gValue;// Color.g
    private float bValue;// Color.b
    private float appearTime = 1.5f;

    /*Auto*/
    private float timer = 0f;

    /*Animator*/
    private bool animeStatus = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("readmeButton Start!");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Appear();// Appear Effect
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // <Animator Off->Hover>
        if (collision.name == "mouse" && animeStatus == false
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().isAlert == false)
        {
            animator.SetBool("isHovering", true);
            animeStatus = true;
        }
        // </Animator Off->Hover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Open Alert>
        if (collision.name == "mouse" && animeStatus == true
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().isAlert == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().WelcomeReadmeAlertOn();
                animator.SetBool("isHovering", false);
                animeStatus = false;
            }
        }
        // </Open Alert>
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // <Animator Hover->Off>
        if (collision.name == "mouse" && animeStatus == true
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().isAlert == false)
        {
            animator.SetBool("isHovering", false);
            animeStatus = false;
        }
        // </Animator Hover->Off>
    }

    private void Appear()
    {
        rValue = gameObject.GetComponent<SpriteRenderer>().material.color.r;
        gValue = gameObject.GetComponent<SpriteRenderer>().material.color.g;
        bValue = gameObject.GetComponent<SpriteRenderer>().material.color.b;
        if (timer > 1f && timer < appearTime)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color
            (rValue, gValue, bValue, Mathf.PingPong(timer / appearTime, 1));// Appear
        }
        if (timer < 1f)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color
               (rValue, gValue, bValue, 0);// Keep Alpha = 0
        }
    }
}
