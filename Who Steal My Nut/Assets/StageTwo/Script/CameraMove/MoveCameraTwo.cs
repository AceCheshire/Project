using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MoveCameraTwo : MonoBehaviour
{
    public Camera cam;
    public StageTwoStatus status;
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
        if ((status.isEarthCreateMode || status.isEnchantCreateMode))
        {
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                    && cam.transform.position.y <= tileNormGround.GetCellCenterWorld(status.startPos).y)
                { cam.transform.position += new Vector3(0, 0.01f, 0); isMoving = true; }
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                    && cam.transform.position.y >= tileNormGround.GetCellCenterWorld(status.endPos).y)
                { cam.transform.position += new Vector3(0, -0.01f, 0); isMoving = true; }
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                    && cam.transform.position.x >= tileNormGround.GetCellCenterWorld(status.startPos).x)
                { cam.transform.position += new Vector3(-0.01f, 0, 0); isMoving = true; }
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                    && cam.transform.position.x <= tileNormGround.GetCellCenterWorld(status.endPos).x + 10)
                { cam.transform.position += new Vector3(0.01f, 0, 0); isMoving = true; }
                else isMoving = false;
            }
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                    && cam.transform.position.y <= tileNormGround.GetCellCenterWorld(status.startPos).y)
                { cam.transform.position += new Vector3(0, 0.01f, 0); isMoving = true; }
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                    && cam.transform.position.y >= tileNormGround.GetCellCenterWorld(status.endPos).y - 5)
                { cam.transform.position += new Vector3(0, -0.01f, 0); isMoving = true; }
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                    && cam.transform.position.x >= tileNormGround.GetCellCenterWorld(status.startPos).x)
                { cam.transform.position += new Vector3(-0.01f, 0, 0); isMoving = true; }
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                    && cam.transform.position.x <= tileNormGround.GetCellCenterWorld(status.endPos).x)
                { cam.transform.position += new Vector3(0.01f, 0, 0); isMoving = true; }
                else isMoving = false;
            }
            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                    && cam.transform.position.y <= tileNormGround.GetCellCenterWorld(status.startPos).y)
                { cam.transform.position += new Vector3(0, 0.01f, 0); isMoving = true; }
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                    && cam.transform.position.y >= tileNormGround.GetCellCenterWorld(status.endPos).y - 5)
                { cam.transform.position += new Vector3(0, -0.01f, 0); isMoving = true; }
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                    && cam.transform.position.x >= tileNormGround.GetCellCenterWorld(status.startPos).x)
                { cam.transform.position += new Vector3(-0.01f, 0, 0); isMoving = true; }
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                    && cam.transform.position.x <= tileNormGround.GetCellCenterWorld(status.endPos).x)
                { cam.transform.position += new Vector3(0.01f, 0, 0); isMoving = true; }
                else isMoving = false;
            }
        }
    }
}
