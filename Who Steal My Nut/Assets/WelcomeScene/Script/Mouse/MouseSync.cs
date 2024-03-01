using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseSync : MonoBehaviour
{
    /*Transform*/
    private Vector3 mousePos;
    private Vector3 worldPos;
    // Update is called once per frame
    void Update()
    {
        MousePos();
    }

    public void MousePos()
    {
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3
            (mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));
        gameObject.transform.position = new Vector3(
            (float)(Mathf.Round(worldPos.x * 100)) / 100 + 0.15f,
            (float)(Mathf.Round(worldPos.y * 100)) / 100 - 0.65f,
            (float)(Mathf.Round(worldPos.z * 100)) / 100);
    }
}
