using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventReadmeButton : MonoBehaviour
{
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
        ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // <Animator Off->Hover>
        if (collision.name == "mouse" && animeStatus == false)
        {
            animator.SetBool("isHovering", true);
            animeStatus = true;
        }
        // </Animator Off->Hover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Exit>
        if (collision.name == "mouse" && animeStatus == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                ; // ******************** Function: Alert ********************
            }
        }
        // </Exit>
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // <Animator Hover->Off>
        if (collision.name == "mouse" && animeStatus == true)
        {
            animator.SetBool("isHovering", false);
            animeStatus = false;
        }
        // </Animator Hover->Off>
    }
}