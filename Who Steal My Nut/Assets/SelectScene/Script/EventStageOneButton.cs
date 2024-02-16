using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventStageOneButton : MonoBehaviour
{
    private bool animeStatus = false;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("HomeButton Start!");
    }

    // Update is called once per frame
    void Update()
    {

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
        // <Open Alert>
        if (collision.name == "mouse" && animeStatus == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                SceneManager.LoadScene("StageOne(Tutorial)");
                animator.SetBool("isHovering", false);
                animeStatus = false;
            }
        }
        // </Open Alert>
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
