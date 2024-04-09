using System.Collections;
//using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LoseJudgeTwo : MonoBehaviour
{
    /*Reference*/
    public StageTwoStatus stageStatus;
    public GameObject nut;
    public Rigidbody2D nutRigidbody;
    public SortStageTwoObject layController;
    public Tilemap tileNormGround;
    public Camera cam;

    /*Switch*/
    private bool isJudged = false;

    IEnumerator coroutine()
    {
        Debug.Log("collision has occurred");
        if (stageStatus.hasShield)
        {
            yield return new WaitForSeconds(2);
            stageStatus.hasShield = false;
            yield break;
        }
        MoveRenderer();
        nutRigidbody.velocity = new Vector3(0, 0, 0);
        nutRigidbody.angularVelocity = 0;
        nutRigidbody.gravityScale = 0;
        cam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        cam.GetComponent<Rigidbody2D>().angularVelocity = 0;
        layController.OneLoseObstacleAlertOn();
        isJudged = true;
        stageStatus.isGameOver = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isJudged)
        {
            if ((stageStatus.obstacleList.Contains(tileNormGround.WorldToCell(nut.transform.position) - new Vector3Int(1, 1, 0))
                || stageStatus.obstacleList.Contains(tileNormGround.WorldToCell(nut.transform.position) - new Vector3Int(2, 2, 0)))
                && JudgeTheSameLine())
            {
                StartCoroutine(coroutine());
            }
            if (stageStatus.endPos == tileNormGround.WorldToCell(nut.transform.position)
                && stageStatus.posList.Contains(stageStatus.endPos - new Vector3Int(1, 1, 0)))
            {
                MoveRenderer();
                nutRigidbody.velocity = new Vector3(0, 0, 0);
                nutRigidbody.angularVelocity = 0;
                nutRigidbody.gravityScale = 0;
                cam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                cam.GetComponent<Rigidbody2D>().angularVelocity = 0;
                layController.OneWinAlertOn();
                isJudged = true;
                stageStatus.isGameOver = true;
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
                stageStatus.isGameOver = true;
            }
            if (GameObject.Find("nut").GetComponent<FallingTwo>().isOver == true
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
                stageStatus.isGameOver = true;
            }
        }
    }

    private void MoveRenderer()
    {
        GameObject.Find("wordRenderer").transform.position -= new Vector3(0.5f, -0.5f, 0);
    }

    private bool JudgeTheSameLine()
    {
        if (nut.GetComponent<FallingTwo>().isSameLineObstacle == true) return true;
        else return false;
    }
}
