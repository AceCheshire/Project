using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockTrigger : MonoBehaviour
{
    /*Static*/
    private GameObject selectButton;

    // Start is called before the first frame update
    void Start()
    {
        selectButton = GameObject.Find("selectButton");
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // <Turn Trigger>
        if (collision.name == "mouse"
            && GameObject.Find("WelcomeSceneSortingOrderConfig").
                GetComponent<SortWelcomeSceneObject>().isAlert == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                selectButton.GetComponent<Rigidbody2D>().gravityScale = 1;
                selectButton.GetComponent<MouseEventSelectButton>().isTriggering = true;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
        // </Turn Trigger>
    }
}
