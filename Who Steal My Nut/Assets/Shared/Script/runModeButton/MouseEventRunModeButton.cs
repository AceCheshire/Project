using UnityEngine;
using UnityEngine.Tilemaps;

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
    public Tilemap isSetMap;
    public bool isPressed = false;

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
            isPressed = true;
        }
        // </Animator OffHover>
        // <Animator OnHover>
        if (collision.name == "mouse" && isModeOn == true && !isOver)
        {
            animator.SetBool("isHovering", true);
            isHovering = true;
            isPressed = true;
        }
        // </Animator OnHover>
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Open Mode Switch>
        if (collision.name == "mouse" && isModeOn == false && isHovering)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isOver && !gameStatusConfig.isGameOver)
            {
                cam.transform.position = new Vector3(0, 0, -10);
                gameStatusConfig.isEnchantCreateMode = false;
                gameStatusConfig.isEarthCreateMode = false;
                gameStatusConfig.isRunningMode = true;
                animator.SetBool("isModeOn", true);
                animator.SetBool("isCanHover", false);
                earthCreateModeButton.SetBool("isModeOn", false);
                isOver = true;
                isModeOn = true;
                for (int i = -20; i <= 20; i++)
                {
                    for (int j = -20; j <= 20; j++)
                    {
                        isSetMap.SetTile(new(i, j, 0), null);
                    }
                }
                //Debug.Log("OpenRunMode");
            }
        }
        // </Open Mode Switch>
        // <Close Mode Switch>
        if (collision.name == "mouse" && isModeOn == true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering && gameStatusConfig.isGameOver)
            {
                gameStatusConfig.isEnchantCreateMode = false;
                gameStatusConfig.isEarthCreateMode = false;
                gameStatusConfig.isRunningMode = false;
                animator.SetBool("isModeOn", false);
                animator.SetBool("isCanHover", false);
                earthCreateModeButton.SetBool("isModeOn", false);
                isOver = true;
                isModeOn = false;
                for (int i = -20; i <= 20; i++)
                {
                    for (int j = -20; j <= 20; j++)
                    {
                        isSetMap.SetTile(new(i, j, 0), null);
                    }
                }
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
