using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*### 这东西开了 RunningMode 之后就不能再打开了嘛 ###*/
/*### 更新：这东西关不掉了，寄 ###*/
/*### 允许重写所有方法 ###*/
public class MouseEventEarthCreateModeButton : MonoBehaviour
{
    /*Animator*/
    private bool isRunning = false;
    private bool isHovering = false;
    private bool isModeOn = false;
    private bool isOver = false;// Avoid Continuous Judgement
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("enchantCreateModeButton Start!");
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = GameObject.Find("gameStatusConfig").
            GetComponent<StageOneStatus>().isRunningMode;
        if (isRunning) animator.SetBool("isRunModeOn", true);
        else
        {
            animator.SetBool("isRunModeOn", false);
            isModeOn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // <Animator OffHover>
        if (collision.name == "mouse" && isModeOn == false && !isOver && !isRunning)
        {
            animator.SetBool("isHovering", true);
            isHovering = true;
        }
        // </Animator OffHover>
        // <Animator OnHover>
        if (collision.name == "mouse" && isModeOn == true && !isOver && !isRunning)
        {
            animator.SetBool("isHovering", true);
            isHovering = true;
        }
        // </Animator OnHover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Open Mode Switch>
        if (collision.name == "mouse" && isModeOn == false && !isRunning)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering)
            {
                GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEnchantCreateMode = false;
                GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEarthCreateMode = true;
                animator.SetBool("isModeOn", true);
                animator.SetBool("isCanHover", false);
                isOver = true;
                isModeOn = true;
                Debug.Log("OpenEarthMode");
            }
        }
        // </Open Mode Switch>
        // <Close Mode Switch>
        if (collision.name == "mouse" && isModeOn == true && !isRunning)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering)
            {
                GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEnchantCreateMode = false;
                GameObject.Find("gameStatusConfig").
                    GetComponent<StageOneStatus>().isEarthCreateMode = false;
                animator.SetBool("isModeOn", false);
                animator.SetBool("isCanHover", false);
                isOver = true;
                isModeOn = false;
                Debug.Log("CloseEarthMode");
            }
        }
        // </Close Mode Switch>
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // <Animator OffHover>
        if (collision.name == "mouse" && isModeOn == false)
        {
            animator.SetBool("isHovering", false);
            animator.SetBool("isCanHover", true);
            isHovering = false;
            isOver = false;
        }
        // </Animator OffHover>
        // <Animator OnHover>
        if (collision.name == "mouse" && isModeOn == true)
        {
            animator.SetBool("isHovering", false);
            animator.SetBool("isCanHover", true);
            isHovering = false;
            isOver = false;
        }
        // </Animator OnHover>
    }
}
