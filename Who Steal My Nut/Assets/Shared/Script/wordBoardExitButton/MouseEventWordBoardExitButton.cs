using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventWordBoardExitButton : MonoBehaviour
{
    private AudioSource Audiodata;
    /*Animator*/
    private bool animeStatus = false;
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
        if (collision.name == "mouse" && animeStatus == false
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                GetComponent<SortWelcomeSceneObject>().isAlert == true)
        {
            Audiodata.Play();
            animator.SetBool("isHovering", true);
            animeStatus = true;
        }
        // </Animator Off->Hover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Close Alert>
        if (collision.name == "mouse" && animeStatus == true
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                GetComponent<SortWelcomeSceneObject>().isAlert == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject.Find("WelcomeSceneSortingOrderConfig").
                    GetComponent<SortWelcomeSceneObject>().WelcomeAlertOff();
                animator.SetBool("isHovering", false);
                animeStatus = false;
            }
        }
        // </Close Alert>
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // <Animator Hover->Off>
        if (collision.name == "mouse" && animeStatus == true
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                GetComponent<SortWelcomeSceneObject>().isAlert == true)
        {
            animator.SetBool("isHovering", false);
            animeStatus = false;
        }
        // </Animator Hover->Off>
    }
}
