using UnityEngine;
using UnityEngine.Tilemaps;

public class GameEarthModeTwo : MonoBehaviour
{
    /*Others*/
    public MouseEventEarthCreateModeButtonTwo earthButton;
    public MouseEventEnchantCreateModeButtonTwo enchantButton;
    public MouseEventRunModeButtonTwo runModeButton;
    //public MouseShieldEarthCreateModeButtonTwo shieldButton;
    //public MouseBoomerEnchantCreateModeButtonTwo boomerButton;
    public MoveCameraTwo camMove;
    public Tilemap startmap;
    public Tilemap sourcemap;
    public Tilemap canSetMap;
    public Tilemap isSetMap;
    public Tilemap tileNormGround;
    public Tilemap tileSyncGround;
    public Tilemap tileLinkGround;
    public Tilemap tileXyzGround;
    public bool isTriggered = false;// Detect the first tile 
    public bool isFirstSet = true;
    private bool isPressed = false;
    private int finishrate;

    private TileBase tile;
    private TileBase enchantedTile;

    public Vector3 mousePos;
    public Vector3 worldPos;
    public Vector3Int currentCellPos;
    private Vector3Int lastCellPos = new Vector3Int();
    public StageTwoStatus secondStage;

    // Start is called before the first frame update
    void Start()
    {
        tile = sourcemap.GetTile(new Vector3Int(-8, -1, 0));// Reference
        enchantedTile = sourcemap.GetTile(new Vector3Int(-9, -2, 0));
    }

    // Update is called once per frame
    void Update()
    {
        isPressed = earthButton.isPressed || enchantButton.isPressed || runModeButton.isPressed;
        if (secondStage.isEarthCreateMode)
        {
            ResetTile();
            SetTile();
        }
        if (secondStage.isEnchantCreateMode)
        {
            ResetEnchantTile();
            SetEnchantTile();
        }
        
        if (secondStage.posList.Count == 0) isFirstSet = true;
        else { isFirstSet = false; }
    }

    public void ResetTile()
    {
        // <GetPosition>
        mousePos = Input.mousePosition;// Mouse position
        worldPos = Camera.main.ScreenToWorldPoint(
            new(mousePos.x, mousePos.y, Camera.main.transform.position.z));
        currentCellPos = new(isSetMap.WorldToCell(worldPos).x - 1,
                             isSetMap.WorldToCell(worldPos).y - 1, 0);
        // </GetPosition>
        if (!isTriggered && !camMove.isMoving)
        {
            if (canSetMap.GetTile(currentCellPos) != null)
                isSetMap.SetTile(currentCellPos, tile);
            isTriggered = true;
        }
        if (currentCellPos != lastCellPos)
        {
            //Debug.Log(currentCellPos);
            for (int i = -20; i <= 20; i++)
            {
                for (int j = -20; j <= 20; j++)
                {
                    isSetMap.SetTile(new(i, j, 0), null);
                }
            }
            if (canSetMap.GetTile(currentCellPos) != null && !camMove.isMoving)
            {
                isSetMap.SetTile(currentCellPos, tile);
            }
        }
        // Refresh tiles
        lastCellPos = currentCellPos;// Refresh position
    }

    public void ResetEnchantTile()
    {
        // <GetPosition>
        mousePos = Input.mousePosition;// Mouse position
        worldPos = Camera.main.ScreenToWorldPoint(
            new(mousePos.x, mousePos.y, Camera.main.transform.position.z));
        currentCellPos = new(isSetMap.WorldToCell(worldPos).x - 1,
                             isSetMap.WorldToCell(worldPos).y - 1, 0);
        // </GetPosition>
        if (!isTriggered && !camMove.isMoving)
        {
            if (canSetMap.GetTile(currentCellPos) != null && tileNormGround.GetTile(currentCellPos) != null)
                isSetMap.SetTile(currentCellPos, enchantedTile);
            isTriggered = true;
        }
        if (currentCellPos != lastCellPos)
        {
            //Debug.Log(currentCellPos);
            for (int i = -20; i <= 20; i++)
            {
                for (int j = -20; j <= 20; j++)
                {
                    isSetMap.SetTile(new(i, j, 0), null);
                }
            }
            if (canSetMap.GetTile(currentCellPos) != null && !camMove.isMoving
                && tileNormGround.GetTile(currentCellPos) != null)
            {
                isSetMap.SetTile(currentCellPos, enchantedTile);
            }
        }
        // Refresh tiles
        lastCellPos = currentCellPos;// Refresh position
    }

    public void SetTile()
    {
        if (isSetMap.GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse0) && !secondStage.posList.Contains(currentCellPos)
            && !secondStage.obstacleList.Contains(currentCellPos)
            && !isPressed && (!isFirstSet || (isFirstSet && startmap.GetTile(currentCellPos) != null)))
        {
            tileNormGround.SetTile(currentCellPos, tile);
            secondStage.canRun = true;
            secondStage.posList.Add(currentCellPos);
            if (PlayerPrefs.GetString("achieve1") != "complete")
            {
                finishrate = PlayerPrefs.GetInt("FinishRate");
                PlayerPrefs.SetString("achieve1", "complete");
                PlayerPrefs.SetInt("FinishRate", finishrate + 20);
            }
            Debug.Log("A tile has been set at " + currentCellPos);
        }
        if (isSetMap.GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse1) && !secondStage.enchantList.Contains(currentCellPos))
        {
            tileNormGround.SetTile(currentCellPos, null);
            if (secondStage.posList.Count == 1) secondStage.canRun = false;
            secondStage.posList.Remove(currentCellPos);
            //Debug.Log("A tile has been removed at " + currentCellPos);
        }
    }

    public void SetEnchantTile()
    {
        if (isSetMap.GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse0) && secondStage.posList.Contains(currentCellPos)
            && !secondStage.enchantList.Contains(currentCellPos) && !isPressed)
        {
            if (secondStage.isSyncCreateMode == true)
            {
                tileSyncGround.SetTile(currentCellPos, enchantedTile);
                secondStage.enchantList.Add(currentCellPos);
            }
            else if (secondStage.isLinkCreateMode == true)
            {
                tileLinkGround.SetTile(currentCellPos, enchantedTile);
                secondStage.boomerList.Add(currentCellPos);
            }
            else if (secondStage.isXyzCreateMode == true)
            {
                tileXyzGround.SetTile(currentCellPos, enchantedTile);
                secondStage.shieldList.Add(currentCellPos);
            }
            
            //Debug.Log("A tile has been set at " + currentCellPos);
        }
        if (isSetMap.GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse1))
        {
            if (secondStage.isSyncCreateMode == true)
            {
                tileSyncGround.SetTile(currentCellPos, null);
                secondStage.enchantList.Remove(currentCellPos);
            }
            else if (secondStage.isLinkCreateMode == true)
            {
                tileLinkGround.SetTile(currentCellPos, null);
                secondStage.boomerList.Remove(currentCellPos);
            }
            else if (secondStage.isXyzCreateMode == true)
            {
                tileXyzGround.SetTile(currentCellPos, null);
                secondStage.shieldList.Remove(currentCellPos);
            }

            //Debug.Log("A tile has been removed at " + currentCellPos);
        }
    }
}
