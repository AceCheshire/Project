using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Falling : MonoBehaviour
{
    public StageOneStatus firstStage;
    public Rigidbody2D NutRigidbody;
    public GameObject NutObject;
    public SpriteRenderer NutRenderer;
    public Tilemap tileNormGround;

    private Vector3 NutDestination;
    private Vector3 WorldOffset = new(0, 0.8f, 0);
    private Vector3 LocalOffset;
    private Vector3Int birthOffset = new Vector3Int(6, 6, 0);
    private Vector3 deltaVelocity = new(0, 2.4f, 0);
    private int Counting=0;
    public int tileNumber;

    public bool isWaitingBirth = true;

    public void Start()
    {
        NutRigidbody.gravityScale = 0;
        NutRenderer.enabled = false;
    }
    public void Update()
    {
        Debug.Log("Counting=" + Counting);
        Debug.Log("tileNumber=" + tileNumber);
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
                if (tileNormGround.WorldToCell((NutObject.transform.position - WorldOffset)) == firstStage.posList[i]
                    && i == Counting)
                {
                    if (tileNumber == Counting)
                    {

                        NutRigidbody.velocity = new Vector3(0, 0, 0);
                        NutRigidbody.angularVelocity = 0;
                        NutRigidbody.gravityScale = 0;
                    }
                    else
                    {
                        Debug.Log("Collision with " + Counting + " tile");
                        //Debug.Log("The Nut is at"+ NutObject.transform.position);
                        //Debug.Log("The No." + i + " tile is at " + firstStage.posList[i]);
                        NutDestination = tileNormGround.CellToWorld(firstStage.posList[i + 1]) -
                            tileNormGround.CellToWorld(firstStage.posList[i]);
                        Bounce(NutDestination);
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
            NutObject.transform.position = tileNormGround.GetCellCenterWorld(firstStage.posList[0] + birthOffset);
            NutRigidbody.gravityScale = 1;
            NutRenderer.enabled = true;
            isWaitingBirth = false;// Teleport to the Birth Position
        }
    }
    public void Bounce(Vector3 Destination)
    {
        float x = Destination.x;
        float y = Destination.y;
        float z = Destination.z;
        float length=Mathf.Sqrt(x*x+y*y+z*z);
        NutRigidbody.velocity = 2f * new Vector3(x / length, y / length, z / length) + length * deltaVelocity;
        //NutRigidbody.angularVelocity = -120 * Destination.y / length;
        //LocalOffset = new Vector3(0.85f * x / length, 0.85f * y / length, 0.85f * z / length);
        //Debug.Log(NutRigidbody.velocity);

    }
}


