using UnityEngine;
using UnityEngine.Tilemaps;

public class GameEarthMode : MonoBehaviour
{
    /*Others*/
    public MouseEventEarthCreateModeButton earthButton;
    public MouseEventEnchantCreateModeButton enchantButton;
    public MouseEventRunModeButton runModeButton;
    public MoveCamera camMove;
    public Tilemap startmap;
    public Tilemap sourcemap;
    public Tilemap canSetMap;
    public Tilemap isSetMap;
    public Tilemap tileNormGround;
    public Tilemap tileSyncGround;
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
    public StageOneStatus gameStatusConfig;

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
        if (gameStatusConfig.isEarthCreateMode)
        {
            ResetTile();
            SetTile();
        }
        if (gameStatusConfig.isEnchantCreateMode)
        {
            ResetEnchantTile();
            SetEnchantTile();
        }
        if (gameStatusConfig.posList.Count == 0) isFirstSet = true;
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
            && Input.GetKey(KeyCode.Mouse0) && !gameStatusConfig.posList.Contains(currentCellPos)
            && !gameStatusConfig.obstacleList.Contains(currentCellPos)
            && !isPressed && (!isFirstSet || (isFirstSet && startmap.GetTile(currentCellPos) != null)))
        {
            tileNormGround.SetTile(currentCellPos, tile);
            gameStatusConfig.canRun = true;
            gameStatusConfig.posList.Add(currentCellPos);
            if (PlayerPrefs.GetString("achieve1") != "complete")
            {
                finishrate = PlayerPrefs.GetInt("FinishRate");
                PlayerPrefs.SetString("achieve1", "complete");
                PlayerPrefs.SetInt("FinishRate", finishrate + 20);
            }
            //Debug.Log("A tile has been set at " + currentCellPos);
        }
        if (isSetMap.GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse1) && !gameStatusConfig.enchantList.Contains(currentCellPos))
        {
            tileNormGround.SetTile(currentCellPos, null);
            if (gameStatusConfig.posList.Count == 1) gameStatusConfig.canRun = false;
            gameStatusConfig.posList.Remove(currentCellPos);
            //Debug.Log("A tile has been removed at " + currentCellPos);
        }
    }

    public void SetEnchantTile()
    {
        if (isSetMap.GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse0) && gameStatusConfig.posList.Contains(currentCellPos)
            && !gameStatusConfig.enchantList.Contains(currentCellPos) && !isPressed)
        {
            tileSyncGround.SetTile(currentCellPos, enchantedTile);
            gameStatusConfig.enchantList.Add(currentCellPos);
            //Debug.Log("A tile has been set at " + currentCellPos);
        }
        if (isSetMap.GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse1))
        {
            tileSyncGround.SetTile(currentCellPos, null);
            gameStatusConfig.enchantList.Remove(currentCellPos);
            //Debug.Log("A tile has been removed at " + currentCellPos);
        }
    }
}
