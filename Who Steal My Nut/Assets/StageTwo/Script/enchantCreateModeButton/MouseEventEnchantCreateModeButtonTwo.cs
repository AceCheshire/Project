using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseEventEnchantCreateModeButtonTwo : MonoBehaviour
{
    /*Animator*/
    private bool isRunning = false;
    private bool isHovering = false;
    private bool isModeOn = false;
    private bool isOver = false;// Avoid Continuous Judgement
    public Animator animator;
    public bool isPressed = false;
    public GameEarthModeTwo gameEarthMode;
    public Tilemap isSetMap;
    public StageTwoStatus secondStage;
    public GameEarthModeTwo earth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Debug.Log("enchantCreateModeButton Start!");
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = secondStage.isRunningMode
            || secondStage.isEarthCreateMode;
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
        // <Open Mode Switch >
        if (collision.name == "mouse" && !isRunning)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering&& secondStage.isXyzCreateMode)
            {
                secondStage.isEnchantCreateMode = true;
                //secondStage.isSyncCreateMode = true;
                secondStage.isLinkCreateMode = true;
                secondStage.isXyzCreateMode = false;
                secondStage.isEarthCreateMode = false;

                animator.SetBool("isSyncModeOn", false);
                animator.SetBool("isXyzModeOn", false);
                animator.SetBool("isLinkModeOn", true);
                animator.SetBool("isCanHover", false);

                earth.enchantedTile = earth.sourcemap.GetTile(new Vector3Int(-10, -3, 0));

                isOver = true;
                isModeOn = true;
                for (int i = -50; i <= 50; i++)
                {
                    for (int j = -50; j <= 50; j++)
                    {
                        isSetMap.SetTile(new(i, j, 0), null);
                    }
                }
                gameEarthMode.isTriggered = false;
                //Debug.Log("OpenEnchantMode Link");
            }
            if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering && secondStage.isSyncCreateMode)
            {
                secondStage.isEnchantCreateMode = true;
                secondStage.isSyncCreateMode = false;
                //secondStage.isLinkCreateMode = true;
                secondStage.isXyzCreateMode = true;
                secondStage.isEarthCreateMode = false;

                animator.SetBool("isSyncModeOn", false);
                animator.SetBool("isLinkModeOn", false);
                animator.SetBool("isXyzModeOn", true);
                animator.SetBool("isCanHover", false);

                earth.enchantedTile = earth.sourcemap.GetTile(new Vector3Int(-11, -4, 0));

                isOver = true;
                isModeOn = true;
                for (int i = -50; i <= 50; i++)
                {
                    for (int j = -50; j <= 50; j++)
                    {
                        isSetMap.SetTile(new(i, j, 0), null);
                    }
                }
                gameEarthMode.isTriggered = false;
                //Debug.Log("OpenEnchantMode Xyz");
            }
            if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering 
                && !secondStage.isSyncCreateMode
                && !secondStage.isXyzCreateMode
                && !secondStage.isLinkCreateMode)
            {
                secondStage.isEnchantCreateMode = true;
                secondStage.isSyncCreateMode = true;
                //secondStage.isLinkCreateMode = true;
                //secondStage.isXyzCreateMode = true;
                secondStage.isEarthCreateMode = false;

                animator.SetBool("isSyncModeOn", true);
                //animator.SetBool("isLinkModeOn", true);
                //animator.SetBool("isXyzModeOn", true);
                animator.SetBool("isCanHover", false);

                earth.enchantedTile = earth.sourcemap.GetTile(new Vector3Int(-9, -2, 0));

                isOver = true;
                isModeOn = true;
                for (int i = -50; i <= 50; i++)
                {
                    for (int j = -50; j <= 50; j++)
                    {
                        isSetMap.SetTile(new(i, j, 0), null);
                    }
                }
                gameEarthMode.isTriggered = false;
                //Debug.Log("OpenEnchantMode Sync");
            }
        }
        // </Open Mode Switch>
        // <Close Mode Switch>
        if (collision.name == "mouse" && isModeOn == true && !isRunning)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isOver && isHovering && secondStage.isLinkCreateMode)
            {
                secondStage.isEnchantCreateMode = false;
                secondStage.isSyncCreateMode = false;
                secondStage.isLinkCreateMode = false;
                secondStage.isXyzCreateMode = false;
                secondStage.isEarthCreateMode = false;

                animator.SetBool("isSyncModeOn", false);
                animator.SetBool("isLinkModeOn", false);
                animator.SetBool("isXyzModeOn", false);
                animator.SetBool("isCanHover", false);

                isOver = true;
                isModeOn = false;
                for (int i = -50; i <= 50; i++)
                {
                    for (int j = -50; j <= 50; j++)
                    {
                        isSetMap.SetTile(new(i, j, 0), null);
                    }
                }
                //Debug.Log("CloseEnchantMode Sync");
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
