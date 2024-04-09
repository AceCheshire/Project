using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class StageTwoStatus : MonoBehaviour
{
    /*Lock & Unlock*/
    public bool isEarthCreateMode = false;
    public bool isEnchantCreateMode = false;
    public bool hasShield = false;
    public bool isSyncCreateMode = false;
    public bool isLinkCreateMode = false;
    public bool isXyzCreateMode = false;
    public bool isRunningMode = false;
    public bool isGameOver = false;
    

    
    /*Timer*/
    public float timer = 0f;
    public float runTimer = 0f;
    private float delayer = 0f;

    /*Unital Grid*/
    public Rigidbody2D NutRigidbody;
    public GameObject NutObject;
    public SpriteRenderer NutRenderer;
    public Tilemap tileNormGround;
    public Tilemap tileObastacleGround;
    public List<Vector3Int> posList;
    public List<Vector3Int> obstacleList;
    public List<Vector3Int> enchantList;
    public List<Vector3Int> shieldList;
    public List<Vector3Int> boomerList;
    public Vector3Int endPos;
    public Vector3Int startPos;
    public SortStageTwoObject layerController;
    public bool canRun = false;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = -10; i <= 10; i++)
        {
            for (int j = -10; j <= 10; j++)
            {
                if (tileObastacleGround.GetTile(new Vector3Int(i, j, 0)) != null)
                {
                    obstacleList.Add(new Vector3Int(i, j, 0));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (layerController.isAlert == false) timer += Time.deltaTime;
        if (timer > delayer)
        {
            delayer += Time.deltaTime;
        }
        if(layerController.isAlert == false && isRunningMode == true)
        {
            runTimer += Time.deltaTime;
        }
    }
}