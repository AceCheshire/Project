using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventAchievementButton : MonoBehaviour
{
    /*Static*/
    private AudioSource Audiodata;
    private float rValue;// Color.r
    private float gValue;// Color.g
    private float bValue;// Color.b
    private float appearTime = 1.5f;

    /*Auto*/
    private float timer = 0f;

    /*Animator*/
    private bool animatorStatus = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Audiodata = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
        if (collision.name == "mouse" && animatorStatus == false
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().isAlert == false)
        {
            Audiodata.Play();
            animator.SetBool("isHovering", true);
            animatorStatus = true;
        }
        // </Animator Off->Hover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Open Alert>
        if (collision.name == "mouse" && animatorStatus == true
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().isAlert == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().WelcomeAchievementAlertOn();
                animator.SetBool("isHovering", false);
                animatorStatus = false;
            }
        }
        // </Open Alert>
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // <Animator Hover->Off>
        if (collision.name == "mouse" && animatorStatus == true
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().isAlert == false)
        {
            animator.SetBool("isHovering", false);
            animatorStatus = false;
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
