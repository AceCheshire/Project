using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFinal : MonoBehaviour
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
            && GameObject.Find("StageOneSortingOrderConfig").
                GetComponent<SortStageOneObject>().isAlert == true)
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
            && GameObject.Find("StageOneSortingOrderConfig").
                GetComponent<SortStageOneObject>().isAlert == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject.Find("StageOneSortingOrderConfig").
                    GetComponent<SortStageOneObject>().OneFinalAlertOff();
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
            && GameObject.Find("StageOneSortingOrderConfig").
                GetComponent<SortStageOneObject>().isAlert == true)
        {
            animator.SetBool("isHovering", false);
            animationStatus = false;
        }
        // </Animator Hover->Off>
    }
}
