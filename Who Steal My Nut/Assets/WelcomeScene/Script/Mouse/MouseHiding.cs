using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHiding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HideCursor();
        Debug.Log("Cursor Visible:" + Cursor.visible);
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    void HideCursor()
    {
        Cursor.visible = false;
    }
}
