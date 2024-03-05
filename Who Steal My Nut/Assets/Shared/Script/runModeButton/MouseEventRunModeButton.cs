using UnityEngine;

public class MouseEventRunModeButton : MonoBehaviour
{
    /*Animator*/
    private bool isHovering = false;
    private bool isModeOn = false;
    private bool isOver = false;// Avoid Continuous Judgement
    private Animator animator;
    public Animator earthCreateModeButton;
    public StageOneStatus gameStatusConfig;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Debug.Log("runModeButton Start!");
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // <Animator OffHover>
        if (collision.name == "mouse" && isModeOn == false && !isOver)
        {
            animator.SetBool("isHovering", true);
            isHovering = true;
        }
        // </Animator OffHover>
        // <Animator OnHover>
        if (collision.name == "mouse" && isModeOn == true && !isOver)
        {
            animator.SetBool("isHovering", true);
            isHovering = true;
        }
        // </Animator OnHover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Open Mode Switch>
        if (collision.name == "mouse" && isModeOn == false && isHovering)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isOver)
            {
                cam.transform.position = new Vector3(0, 0, -10);
                gameStatusConfig.isEnchantCreateMode = false;
                gameStatusConfig.isEarthCreateMode = false;
                gameStatusConfig.isRunningMode = true;
                animator.SetBool("isModeOn", true);
                animator.SetBool("isCanHover", false);
                isOver = true;
                isModeOn = true;
                //Debug.Log("OpenRunMode");
            }
        }
        // </Open Mode Switch>
        // <Close Mode Switch>
        if (collision.name == "mouse" && isModeOn == true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering)
            {
                gameStatusConfig.isEnchantCreateMode = false;
                gameStatusConfig.isEarthCreateMode = false;
                gameStatusConfig.isRunningMode = false;
                animator.SetBool("isModeOn", false);
                animator.SetBool("isCanHover", false);
                earthCreateModeButton.SetBool("isModeOn", false);
                isOver = true;
                isModeOn = false;
                //Debug.Log("CloseRunMode");
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
