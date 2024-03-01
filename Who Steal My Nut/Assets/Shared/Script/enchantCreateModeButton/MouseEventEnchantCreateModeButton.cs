using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*### 这东西完全没做嘛 ###*/
/*### 允许重写所有方法 ###*/
public class MouseEventEnchantCreateModeButton : MonoBehaviour
{
    /*Animator*/
    private bool isAnimationOn = false;
    //private bool isModeOn = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("earthCreateModeButton Start!");
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // <Animator Off->Hover>
        if (collision.name == "mouse" && isAnimationOn == false
            && GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEarthCreateMode == false)
        {
            animator.SetBool("isAnimeOn", true);
            isAnimationOn = true;
        }
        // </Animator Off->Hover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Open Mode Switch>
        if (collision.name == "mouse" 
            && GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEarthCreateMode == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEarthCreateMode = true;
                GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEnchantCreateMode = false;
                animator.SetBool("isAnimeOn", true);
                isAnimationOn = true;
            }
        }
        // </Open Mode Switch>
        // <Close Mode Switch>
        if (collision.name == "mouse"
            && GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEarthCreateMode == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEarthCreateMode = false;
            }
        }
        // </Close Mode Switch>
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // <Animator Hover->Off>
        if (collision.name == "mouse" && isAnimationOn == true
            && GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEarthCreateMode == false)
        {
            animator.SetBool("isAnimeOn", false);
            isAnimationOn = false;
        }
        // </Animator Hover->Off>
    }
}
