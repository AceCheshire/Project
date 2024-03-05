using UnityEngine;
using UnityEngine.Tilemaps;

public class GameEarthMode : MonoBehaviour
{


    /*Others*/
    public MouseEventEarthCreateModeButton button;
    public MoveCamera camMove;
    public Tilemap startmap;
    public Tilemap sourcemap;
    public Tilemap canSetMap;
    public Tilemap isSetMap;
    public Tilemap tileNormGround;
    public bool isTriggered = false;// Detect the first tile 
    public bool isFirstSet = true;

    private TileBase tile;

    public Vector3 mousePos;
    public Vector3 worldPos;
    public Vector3Int currentCellPos;
    private Vector3Int lastCellPos=new Vector3Int();
    public StageOneStatus gameStatusConfig;

    // Start is called before the first frame update
    void Start()
    {
        tile = sourcemap.GetTile(new Vector3Int(-8, -1, 0));// Reference
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatusConfig.isEarthCreateMode)
        {
            ResetTile();
            SetTile();
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

    public void SetTile()
    {
        if (isSetMap.GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse0) && !gameStatusConfig.posList.Contains(currentCellPos)
            && !gameStatusConfig.obstacleList.Contains(currentCellPos)
            && !button.isPressed && (!isFirstSet || (isFirstSet && startmap.GetTile(currentCellPos) != null)))
        {
            tileNormGround.SetTile(currentCellPos, tile);
            gameStatusConfig.posList.Add(currentCellPos);
            //Debug.Log("A tile has been set at " + currentCellPos);
        }
        if (isSetMap.GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse1))
        {
            tileNormGround.SetTile(currentCellPos, null);
            gameStatusConfig.posList.Remove(currentCellPos);
            //Debug.Log("A tile has been removed at " + currentCellPos);
        }
    }
}
