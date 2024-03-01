using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventWordBoardExitButton : MonoBehaviour
{
    /*Audio*/
    private AudioSource Audiodata;

    /*Animator*/
    private bool animationStatus = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Audiodata = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        Debug.Log("wordBoard Start!");
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // <Animator Off->Hover>
        if (collision.name == "mouse" && animationStatus == false
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                GetComponent<SortWelcomeSceneObject>().isAlert == true)
        {
            Audiodata.Play();
            animator.SetBool("isHovering", true);
            animationStatus = true;
        }
        // </Animator Off->Hover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Close Alert>
        if (collision.name == "mouse" && animationStatus == true
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                GetComponent<SortWelcomeSceneObject>().isAlert == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().WelcomeAlertOff();
                animator.SetBool("isHovering", false);
                animationStatus = false;
            }
        }
        // </Close Alert>
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // <Animator Hover->Off>
        if (collision.name == "mouse" && animationStatus == true
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                GetComponent<SortWelcomeSceneObject>().isAlert == true)
        {
            animator.SetBool("isHovering", false);
            animationStatus = false;
        }
        // </Animator Hover->Off>
    }
}
