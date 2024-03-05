using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LoseJudge : MonoBehaviour
{
    /*Reference*/
    public StageOneStatus stageStatus;
    public GameObject nut;
    public Rigidbody2D nutRigidbody;
    public SortStageOneObject layController;
    public Tilemap tileNormGround;
    public Camera cam;

    /*Switch*/
    private bool isJudged = false;

    // Update is called once per frame
    void Update()
    {
        if(!isJudged)
        {
            if ((stageStatus.obstacleList.Contains(tileNormGround.WorldToCell(nut.transform.position) - new Vector3Int(1, 1, 0))
                || stageStatus.obstacleList.Contains(tileNormGround.WorldToCell(nut.transform.position) - new Vector3Int(2, 2, 0)))
                && JudgeTheSameLine())
            {
                MoveRenderer();
                nutRigidbody.velocity = new Vector3(0, 0, 0);
                nutRigidbody.angularVelocity = 0;
                nutRigidbody.gravityScale = 0;
                cam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                cam.GetComponent<Rigidbody2D>().angularVelocity = 0;
                layController.OneLoseObstacleAlertOn();
                isJudged = true;
            }
            if (stageStatus.endPos == tileNormGround.WorldToCell(nut.transform.position))
            {
                MoveRenderer();
                nutRigidbody.velocity = new Vector3(0, 0, 0);
                nutRigidbody.angularVelocity = 0;
                nutRigidbody.gravityScale = 0;
                cam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                cam.GetComponent<Rigidbody2D>().angularVelocity = 0;
                layController.OneWinAlertOn();
                isJudged = true;
            }
            if (nut.transform.position.y <= -30)
            {
                MoveRenderer();
                nutRigidbody.velocity = new Vector3(0, 0, 0);
                nutRigidbody.angularVelocity = 0;
                nutRigidbody.gravityScale = 0;
                cam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                cam.GetComponent<Rigidbody2D>().angularVelocity = 0;
                layController.OneLoseFallAlertOn();
                isJudged = true;
            }
            if(GameObject.Find("nut").GetComponent<Falling>().isOver==true
                && stageStatus.endPos != tileNormGround.WorldToCell(nut.transform.position))
            {
                MoveRenderer();
                nutRigidbody.velocity = new Vector3(0, 0, 0);
                nutRigidbody.angularVelocity = 0;
                nutRigidbody.gravityScale = 0;
                cam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                cam.GetComponent<Rigidbody2D>().angularVelocity = 0;
                layController.OneLoseStopAlertOn();
                isJudged = true;
            }
        }
    }

    private void MoveRenderer()
    {
        GameObject.Find("wordRenderer").transform.position -= new Vector3(0.5f, -0.5f, 0);
    }

    private bool JudgeTheSameLine()
    {
        if(nut.GetComponent<Falling>().isSameLineObstacle==true)return true;
        else return false;
    }
}
