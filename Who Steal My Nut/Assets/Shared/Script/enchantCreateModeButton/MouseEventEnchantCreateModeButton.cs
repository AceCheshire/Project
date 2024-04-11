using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseEventEnchantCreateModeButton : MonoBehaviour
{
    /*Animator*/
    private bool isRunning = false;
    private bool isHovering = false;
    private bool isModeOn = false;
    private bool isOver = false;// Avoid Continuous Judgement
    private Animator animator;
    public bool isPressed = false;
    public GameEarthMode gameEarthMode;
    public Tilemap isSetMap;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Debug.Log("enchantCreateModeButton Start!");
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = GameObject.Find("gameStatusConfig").
            GetComponent<StageOneStatus>().isRunningMode
            || GameObject.Find("gameStatusConfig").
            GetComponent<StageOneStatus>().isEarthCreateMode;
        if (isRunning) animator.SetBool("isRunModeOn", true);
        else
        {
            animator.SetBool("isRunModeOn", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // <Animator OffHover>
        if (collision.name == "mouse" && isModeOn == false && !isOver && !isRunning)
        {
            isPressed = true;
            animator.SetBool("isHovering", true);
            isHovering = true;
        }
        // </Animator OffHover>
        // <Animator OnHover>
        if (collision.name == "mouse" && isModeOn == true && !isOver && !isRunning)
        {
            isPressed = true;
            animator.SetBool("isHovering", true);
            isHovering = true;
        }
        // </Animator OnHover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameObject.Find("gameStatusConfig").
    GetComponent<StageOneStatus>().isCanEnchantCreateMode)
        {
            // <Open Mode Switch Sync>
            if (collision.name == "mouse" && isModeOn == false && !isRunning)
            {
                if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering)
                {
                    GameObject.Find("gameStatusConfig").
                        GetComponent<StageOneStatus>().isEnchantCreateMode = true;
                    GameObject.Find("gameStatusConfig").
                        GetComponent<StageOneStatus>().isEarthCreateMode = false;
                    animator.SetBool("isSyncModeOn", true);
                    animator.SetBool("isCanHover", false);
                    isOver = true;
                    isModeOn = true;
                    for (int i = -30; i <= 30; i++)
                    {
                        for (int j = -30; j <= 30; j++)
                        {
                            isSetMap.SetTile(new(i, j, 0), null);
                        }
                    }
                    gameEarthMode.isTriggered = false;
                    //Debug.Log("OpenEnchantMode Sync");
                }
            }
            // </Open Mode Switch Sync>
            // <Close Mode Switch>
            if (collision.name == "mouse" && isModeOn == true && !isRunning)
            {
                if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering)
                {
                    GameObject.Find("gameStatusConfig").
                        GetComponent<StageOneStatus>().isEnchantCreateMode = false;
                    GameObject.Find("gameStatusConfig").
                        GetComponent<StageOneStatus>().isEarthCreateMode = false;
                    animator.SetBool("isSyncModeOn", false);
                    animator.SetBool("isCanHover", false);
                    isOver = true;
                    isModeOn = false;
                    for (int i = -30; i <= 30; i++)
                    {
                        for (int j = -30; j <= 30; j++)
                        {
                            isSetMap.SetTile(new(i, j, 0), null);
                        }
                    }
                    //Debug.Log("CloseEnchantMode Sync");
                }
            }
            // </Close Mode Switch>
        }
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
            isPressed = false;
        }
        // </Animator OffHover>
        // <Animator OnHover>
        if (collision.name == "mouse" && isModeOn == true)
        {
            animator.SetBool("isHovering", false);
            animator.SetBool("isCanHover", true);
            isHovering = false;
            isOver = false;
            isPressed = false;
        }
        // </Animator OnHover>
    }
}
