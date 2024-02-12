using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventExitButton : MonoBehaviour
{
    /*Animator*/
    private bool animeStatus = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("exitButton Start!");
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
                GetComponent<SortWelcomeSceneObject>().isAlert == false)
        {
            animator.SetBool("isHovering", true);
            animeStatus = true;
        }
        // </Animator Off->Hover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Exit>
        if (collision.name == "mouse" && animeStatus == true 
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                GetComponent<SortWelcomeSceneObject>().isAlert == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                animator.SetBool("isHovering", false);
                animeStatus = false;
                Application.Quit();
            }
        }
        // </Exit>
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
}
