using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventGuideButtonTwo : MonoBehaviour
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
        //Debug.Log("GuideButton Start!");
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // <Animator Off->Hover>
        if (collision.name == "mouse" && animationStatus == false)
        {
            Audiodata.Play();
            animator.SetBool("isHovering", true);
            animationStatus = true;
        }
        // </Animator Off->Hover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Open Alert>
        if (collision.name == "mouse" && animationStatus == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))

            {
                GameObject.Find("StageTwoSortingOrderConfig").
                    GetComponent<SortStageTwoObject>().OneGuideAlertOn();
                animator.SetBool("isHovering", false);
                animationStatus = false;
            }
        }
        // </Open Alert>
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // <Animator Hover->Off>
        if (collision.name == "mouse" && animationStatus == true)
        {
            animator.SetBool("isHovering", false);
            animationStatus = false;
        }
        // </Animator Hover->Off>
    }
}
