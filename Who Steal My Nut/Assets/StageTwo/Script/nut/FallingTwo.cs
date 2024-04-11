using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class FallingTwo : MonoBehaviour
{
    public StageTwoStatus secondStage;
    public Rigidbody2D nutRigidbody;
    public GameObject nutObject;
    public SpriteRenderer nutRenderer;
    public Tilemap tileNormGround;
    public Tilemap tileSyncGround;
    public Tilemap tileLinkGround;
    public Tilemap tileXyzGround;
    public Tilemap tileObastacleGround;
    public Camera cam;
    public AudioSource sound;

    private Vector3 nutDestination;
    private float communicateX;
    private float communicateY;
    private Vector3 worldOffset = new(0f, 1.2f, 0);
    private Vector3 localOffset;
    private Vector3Int birthOffset = new Vector3Int(6, 6, 0);
    private Vector3 deltaVelocity = new(0, 2.4f, 0);
    private int Counting = 0;
    public int tileNumber;
    private float rValue;
    private float gValue;
    private float bValue;

    public bool isWaitingBirth = true;
    public bool isOver = false;
    public bool isSameLineObstacle = false;

    public void Start()
    {
        nutRigidbody.gravityScale = 0;
        nutRenderer.enabled = false;
        rValue = gameObject.GetComponent<SpriteRenderer>().material.color.r;
        gValue = gameObject.GetComponent<SpriteRenderer>().material.color.g;
        bValue = gameObject.GetComponent<SpriteRenderer>().material.color.b;
    }

    public void Update()
    {
        if (secondStage.hasShield) gameObject.GetComponent<SpriteRenderer>().material.color = new Color
            (rValue - 0.75f, gValue - 0.5f, bValue - 0.25f, 1);
        else gameObject.GetComponent<SpriteRenderer>().material.color = new Color
            (rValue, gValue, bValue, 1);
        //Debug.Log("Counting=" + Counting);
        //Debug.Log("tileNumber=" + tileNumber);
        //Debug.Log(firstStage.isRunningMode);
        //Debug.Log("isWating:" + isWaitingBirth);
        if (secondStage.isRunningMode == true)
        {
            /*for(int i=0;i< firstStage.posList.Count; i++)
            {
                Debug.Log("The No." + i + " tile is at " + firstStage.posList[i]);
            }*/
            FirstDown();
            tileNumber = secondStage.posList.Count - 1;
        }
        if (isWaitingBirth == false)
        {
            for (int i = 0; i <= tileNumber; i++)
            {
                if (tileNormGround.WorldToCell((nutObject.transform.position - worldOffset)) == secondStage.posList[i]
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
                        nutDestination = tileNormGround.CellToWorld(secondStage.posList[i + 1]) -
                            tileNormGround.CellToWorld(secondStage.posList[i]);

                        if (secondStage.enchantList.Contains(secondStage.posList[i])
                            && secondStage.enchantList.Contains(secondStage.posList[i + 1]))
                        {
                            if (Mathf.Abs(nutDestination.x) <= 5.2f && Mathf.Abs(nutDestination.y) <= 2.6f)
                                EnchantBounce(nutDestination, i);
                            else
                            {
                                sound.Play();
                                Bounce(nutDestination);
                            }
                        }
                        else if (secondStage.shieldList.Contains(secondStage.posList[i]))
                        {
                            secondStage.hasShield = true;
                            sound.Play();
                            Bounce(nutDestination);
                        }
                        else if (secondStage.boomerList.Contains(secondStage.posList[i]))
                        {
                            for (int x = -1; x <= 1; x++)
                            {
                                for (int y = -2; y <= 0; y++)
                                {
                                    Vector3Int order = secondStage.posList[i] + new Vector3Int(x, y, 0);
                                    if (tileObastacleGround.GetTile(order) != null)
                                    {
                                        tileObastacleGround.SetTile(order, null);
                                        secondStage.obstacleList.Remove(order);
                                    }
                                }
                            }
                            sound.Play();
                            Bounce(nutDestination);
                        }
                        else
                        {
                            sound.Play();
                            Bounce(nutDestination);
                        }
                        SameLineJudge(i);
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
            nutObject.transform.position = tileNormGround.GetCellCenterWorld(secondStage.posList[0] + birthOffset);
            nutRigidbody.gravityScale = 1;
            nutRenderer.enabled = true;
            isWaitingBirth = false;// Teleport to the Birth Position
        }
    }

    public void EnchantBounce(Vector3 Destination, int i)
    {
        nutRigidbody.velocity = new Vector2(0, nutRigidbody.velocity.y);
        nutObject.transform.position = tileNormGround.CellToWorld(secondStage.posList[i + 1] + new Vector3Int(2, 2, 0));
        EnchantCameraSync(Destination);
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
        CameraSync(x, y, length);
    }

    private void SameLineJudge(int i)
    {
        if (secondStage.enchantList.Contains(secondStage.posList[i])) isSameLineObstacle = false;
        else
        {
            if (secondStage.posList[i + 1].x >= secondStage.posList[i].x)
            {
                for (int j = secondStage.posList[i].x; j <= secondStage.posList[i + 1].x; j++)
                {
                    if (secondStage.posList[i + 1].y >= secondStage.posList[i].y)
                    {
                        for (int k = secondStage.posList[i].y; k <= secondStage.posList[i + 1].y; k++)
                        {
                            if (secondStage.obstacleList.Contains(new Vector3Int(j, k, 0)))
                                isSameLineObstacle = true;
                        }
                    }
                    else
                    {
                        for (int k = secondStage.posList[i + 1].y; k <= secondStage.posList[i].y; k++)
                        {
                            if (secondStage.obstacleList.Contains(new Vector3Int(j, k, 0)))
                                isSameLineObstacle = true;
                        }
                    }
                }
            }
            else
            {
                for (int j = secondStage.posList[i + 1].x; j <= secondStage.posList[i].x; j++)
                {
                    if (secondStage.posList[i + 1].y >= secondStage.posList[i].y)
                    {
                        for (int k = secondStage.posList[i].y; k <= secondStage.posList[i + 1].y; k++)
                        {
                            if (secondStage.obstacleList.Contains(new Vector3Int(j, k, 0)))
                                isSameLineObstacle = true;
                        }
                    }
                    else
                    {
                        for (int k = secondStage.posList[i + 1].y; k <= secondStage.posList[i].y; k++)
                        {
                            if (secondStage.obstacleList.Contains(new Vector3Int(j, k, 0)))
                                isSameLineObstacle = true;
                        }
                    }
                }
            }
        }
    }

    private void CameraSync(float x, float y, float length)
    {
        float frontierX = 0f;
        float frontierY = 0f;
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            frontierX = tileNormGround.GetCellCenterWorld(secondStage.endPos).x + 10;
            frontierY = tileNormGround.GetCellCenterWorld(secondStage.endPos).y;
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            frontierX = tileNormGround.GetCellCenterWorld(secondStage.endPos).x;
            frontierY = tileNormGround.GetCellCenterWorld(secondStage.endPos).y - 5;
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            frontierX = tileNormGround.GetCellCenterWorld(secondStage.endPos).x;
            frontierY = tileNormGround.GetCellCenterWorld(secondStage.endPos).y - 5;
        }

        cam.GetComponent<Rigidbody2D>().velocity = 2f * new Vector2(x / length, y / length);
        if (cam.transform.position.x >= frontierX)
            cam.GetComponent<Rigidbody2D>().velocity -= 2f * new Vector2(x / length, 0);
        if (cam.transform.position.y <= frontierY)
            cam.GetComponent<Rigidbody2D>().velocity -= 2f * new Vector2(0, y / length);
    }

    private void EnchantCameraSync(Vector3 Destination)
    {
        float frontierX = 0f;
        float frontierY = 0f;
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            frontierX = tileNormGround.GetCellCenterWorld(secondStage.endPos).x + 10;
            frontierY = tileNormGround.GetCellCenterWorld(secondStage.endPos).y;
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            frontierX = tileNormGround.GetCellCenterWorld(secondStage.endPos).x;
            frontierY = tileNormGround.GetCellCenterWorld(secondStage.endPos).y - 5;
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            frontierX = tileNormGround.GetCellCenterWorld(secondStage.endPos).x;
            frontierY = tileNormGround.GetCellCenterWorld(secondStage.endPos).y - 5;
        }

        if (cam.transform.position.x >= frontierX)
            cam.transform.position = new Vector3
                (cam.transform.position.x,
                cam.transform.position.y + Destination.y,
                cam.transform.position.z + Destination.z);
        else if (cam.transform.position.y <= frontierY)
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
}
