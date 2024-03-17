using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class FallingTwo : MonoBehaviour
{
    public StageTwoStatus firstStage;
    public Rigidbody2D nutRigidbody;
    public GameObject nutObject;
    public SpriteRenderer nutRenderer;
    public Tilemap tileNormGround;
    public Tilemap tileSyncGround;
    public Camera cam;
    public AudioSource sound;

    private Vector3 nutDestination;
    private float communicateX;
    private float communicateY;
    private Vector3 worldOffset = new(0f, 1.2f, 0);
    private Vector3 localOffset;
    private Vector3Int birthOffset = new Vector3Int(6, 6, 0);
    private Vector3 deltaVelocity = new(0, 2.4f, 0);
    private int Counting=0;
    public int tileNumber;

    public bool isWaitingBirth = true;
    public bool isOver = false;
    public bool isSameLineObstacle = false;

    public void Start()
    {
        nutRigidbody.gravityScale = 0;
        nutRenderer.enabled = false;
    }
    public void Update()
    {
        //Debug.Log("Counting=" + Counting);
        //Debug.Log("tileNumber=" + tileNumber);
        //Debug.Log(firstStage.isRunningMode);
        //Debug.Log("isWating:" + isWaitingBirth);
        if (firstStage.isRunningMode == true)
        {
            /*for(int i=0;i< firstStage.posList.Count; i++)
            {
                Debug.Log("The No." + i + " tile is at " + firstStage.posList[i]);
            }*/
            FirstDown();
            tileNumber = firstStage.posList.Count - 1;
        }
        if (isWaitingBirth == false)
        {
            for (int i = 0; i <= tileNumber; i++)
            {
                if (tileNormGround.WorldToCell((nutObject.transform.position - worldOffset)) == firstStage.posList[i]
                    && i == Counting
                    && nutRigidbody.velocity.y <= 0)
                {
                    isSameLineObstacle = false;
                    if (tileNumber == Counting)
                    {
                        sound.Play();
                        nutRigidbody.velocity = new Vector3(0, 0, 0);
                        nutRigidbody.angularVelocity = 0;
                        nutRigidbody.gravityScale = 0;
                        cam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                        cam.GetComponent<Rigidbody2D>().angularVelocity = 0;
                        isOver = true;
                    }
                    else
                    {
                        //Debug.Log("Collision with " + Counting + " tile");
                        //Debug.Log("The Nut is at"+ nutObject.transform.position);
                        //Debug.Log("The No." + i + " tile is at " + firstStage.posList[i]);
                        nutDestination = tileNormGround.CellToWorld(firstStage.posList[i + 1]) -
                            tileNormGround.CellToWorld(firstStage.posList[i]);
                        SameLineJudge(i);
                        if (!firstStage.enchantList.Contains(firstStage.posList[i])
                            || !firstStage.enchantList.Contains(firstStage.posList[i + 1]))
                        {
                            sound.Play();
                            Bounce(nutDestination);
                        }
                        else if (firstStage.enchantList.Contains(firstStage.posList[i])
                            && firstStage.enchantList.Contains(firstStage.posList[i + 1]))
                        {
                            if (Mathf.Abs(nutDestination.x) <= 5.2f && Mathf.Abs(nutDestination.y) <= 2.6f)
                                EnchantBounce(nutDestination, i);
                            else
                            {
                                sound.Play();
                                Bounce(nutDestination); 
                            }
                        }
                        Counting++;
                    }
                }
            }
        }
    }
    public void FirstDown()
    {
        if (isWaitingBirth == true)
        {
            nutObject.transform.position = tileNormGround.GetCellCenterWorld(firstStage.posList[0] + birthOffset);
            nutRigidbody.gravityScale = 1;
            nutRenderer.enabled = true;
            isWaitingBirth = false;// Teleport to the Birth Position
        }
    }
    public void EnchantBounce(Vector3 Destination, int i)
    {
        nutRigidbody.velocity = new Vector2(0, nutRigidbody.velocity.y);
        nutObject.transform.position = tileNormGround.CellToWorld(firstStage.posList[i + 1] + new Vector3Int(2, 2, 0));
        if (cam.transform.position.x >= tileNormGround.GetCellCenterWorld(firstStage.endPos).x)
            cam.transform.position = new Vector3
                (cam.transform.position.x,
                cam.transform.position.y + Destination.y,
                cam.transform.position.z + Destination.z);
        else if (cam.transform.position.y <= tileNormGround.GetCellCenterWorld(firstStage.endPos).y)
            cam.transform.position = new Vector3
                (cam.transform.position.x + Destination.x,
                cam.transform.position.y,
                cam.transform.position.z + Destination.z);
        else
        {
            cam.transform.position =
            new Vector3(cam.transform.position.x + Destination.x,
            cam.transform.position.y + Destination.y,
            cam.transform.position.z + Destination.z);
        }
    }

    public void Bounce(Vector3 Destination)
    {
        float x = Destination.x;
        if (Destination.x >= 0) x = Mathf.Min(Destination.x, 2.6f);
        else if (Destination.x <= 0) x = Mathf.Max(Destination.x, -2.6f);
        float y = Destination.y;
        if (Destination.y >= 0) y = Mathf.Min(Destination.y, 1.3f);
        else if (Destination.y <= 0) y = Mathf.Max(Destination.y, -1.3f);
        float z = Destination.z;
        float length = Mathf.Sqrt(x * x + y * y + z * z);
        communicateX = x;
        communicateY = y;
        //Debug.Log("x:" + x + "y:" + y + "z:" + z);
        nutRigidbody.velocity = 2f * new Vector3(x / length, y / length, z / length) + length * deltaVelocity;
        //nutRigidbody.angularVelocity = -120 * Destination.y / length;
        //localOffset = new Vector3(0.85f * x / length, 0.85f * y / length, 0.85f * z / length);
        //Debug.Log(nutRigidbody.velocity);
        cam.GetComponent<Rigidbody2D>().velocity = 2f * new Vector2(x / length, y / length);
        if (cam.transform.position.x >= tileNormGround.GetCellCenterWorld(firstStage.endPos).x)
            cam.GetComponent<Rigidbody2D>().velocity -= 2f * new Vector2(x / length, 0);
        if (cam.transform.position.y <= tileNormGround.GetCellCenterWorld(firstStage.endPos).y)
            cam.GetComponent<Rigidbody2D>().velocity -= 2f * new Vector2(0, y / length);
    }

    private void SameLineJudge(int i)
    {
        if (firstStage.enchantList.Contains(firstStage.posList[i])) isSameLineObstacle = false;
        else
        {
            if (firstStage.posList[i + 1].x >= firstStage.posList[i].x)
            {
                for (int j = firstStage.posList[i].x; j <= firstStage.posList[i + 1].x; j++)
                {
                    if (firstStage.posList[i + 1].y >= firstStage.posList[i].y)
                    {
                        for (int k = firstStage.posList[i].y; k <= firstStage.posList[i + 1].y; k++)
                        {
                            if (firstStage.obstacleList.Contains(new Vector3Int(j, k, 0)))
                                isSameLineObstacle = true;
                        }
                    }
                    else
                    {
                        for (int k = firstStage.posList[i + 1].y; k <= firstStage.posList[i].y; k++)
                        {
                            if (firstStage.obstacleList.Contains(new Vector3Int(j, k, 0)))
                                isSameLineObstacle = true;
                        }
                    }
                }
            }
            else
            {
                for (int j = firstStage.posList[i + 1].x; j <= firstStage.posList[i].x; j++)
                {
                    if (firstStage.posList[i + 1].y >= firstStage.posList[i].y)
                    {
                        for (int k = firstStage.posList[i].y; k <= firstStage.posList[i + 1].y; k++)
                        {
                            if (firstStage.obstacleList.Contains(new Vector3Int(j, k, 0)))
                                isSameLineObstacle = true;
                        }
                    }
                    else
                    {
                        for (int k = firstStage.posList[i + 1].y; k <= firstStage.posList[i].y; k++)
                        {
                            if (firstStage.obstacleList.Contains(new Vector3Int(j, k, 0)))
                                isSameLineObstacle = true;
                        }
                    }
                }
            }
        }
    }
}
