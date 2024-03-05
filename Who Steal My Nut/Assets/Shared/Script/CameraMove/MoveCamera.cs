using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveCamera : MonoBehaviour
{
    public Camera cam;
    public StageOneStatus status;
    public Tilemap tileNormGround;
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        if (status.isEarthCreateMode || status.isEnchantCreateMode)
        {
            if (Input.GetKey(KeyCode.UpArrow)
                && cam.transform.position.y <= tileNormGround.GetCellCenterWorld(status.startPos).y) 
            { cam.transform.position += new Vector3(0, 0.01f, 0); isMoving = true; }
            if (Input.GetKey(KeyCode.DownArrow)
                && cam.transform.position.y >= tileNormGround.GetCellCenterWorld(status.endPos).y) 
            { cam.transform.position += new Vector3(0, -0.01f, 0); isMoving = true; }
            if (Input.GetKey(KeyCode.LeftArrow)
                && cam.transform.position.x >= tileNormGround.GetCellCenterWorld(status.startPos).x) 
            { cam.transform.position += new Vector3(-0.01f, 0, 0); isMoving = true; }
            if (Input.GetKey(KeyCode.RightArrow)
                && cam.transform.position.x <= tileNormGround.GetCellCenterWorld(status.endPos).x) 
            { cam.transform.position += new Vector3(0.01f, 0, 0); isMoving = true; }
            else isMoving = false;
        }
    }
}
