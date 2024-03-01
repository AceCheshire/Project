using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseSync : MonoBehaviour
{
    /*Transform*/
    private Vector3 mousePos = new Vector3(0, 0, 0);
    private Vector3 worldPos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        MousePos();
    }

    // Update is called once per frame
    void Update()
    {
        MousePos();
    }

    private void MousePos()
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
