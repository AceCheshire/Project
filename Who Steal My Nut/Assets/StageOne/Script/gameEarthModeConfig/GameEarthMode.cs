using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*### 这东西完全关不掉嘛 ###*/
/*### 允许重写所有方法 ###*/
public class GameEarthMode : MonoBehaviour
{
    /*External Infomation*/
    private bool isEarthMode;

    /*Others*/
    private GameObject sourcemap;
    private GameObject canSetMap;
    private GameObject isSetMap;
    private bool isTriggered = false;// Detect the first tile 

    /*Reference Of Tile*/
    private TileBase tile;

    /*Vectors Of Mouse Position*/
    private Vector3 mousePos;
    private Vector3 worldPos;
    private Vector3Int currentCellPos;
    private Vector3Int lastCellPos=new Vector3Int();

    // Start is called before the first frame update
    void Start()
    {
        sourcemap = GameObject.Find("tileFloatGround");
        canSetMap = GameObject.Find("tileCanSetGround");
        isSetMap = GameObject.Find("tileIsSetGround");
        if (sourcemap.GetComponent<Tilemap>().GetTile(new Vector3Int(-8, -1, 0)) == null)
            Debug.Log("NULL!");
        else Debug.Log("TRUE!");
        tile = sourcemap.GetComponent<Tilemap>().GetTile(new Vector3Int(-8, -1, 0));// Reference
    }

    // Update is called once per frame
    void Update()
    {
        isEarthMode = GameObject.Find("gameStatusConfig").
            GetComponent<StageOneStatus>().isEarthCreateMode;
        if (isEarthMode)
        {
            ResetTile();
            SetTile();
        }
    }

    private void ResetTile()
    {
        // <GetPosition>
        mousePos = Input.mousePosition;// Mouse position
        worldPos = Camera.main.ScreenToWorldPoint(
            new(mousePos.x, mousePos.y, Camera.main.transform.position.z));
        currentCellPos = new(isSetMap.GetComponent<Tilemap>().WorldToCell(worldPos).x - 1,
                             isSetMap.GetComponent<Tilemap>().WorldToCell(worldPos).y - 1, 0);
        // </GetPosition>
        if (!isTriggered)
        {
            if (canSetMap.GetComponent<Tilemap>().GetTile(currentCellPos) != null)
                isSetMap.GetComponent<Tilemap>().SetTile(currentCellPos, tile);
            isTriggered = true;
        }
        if (currentCellPos != lastCellPos)
        {
            Debug.Log(currentCellPos);
            for (int i = -10; i <= 10; i++)
            {
                for (int j = -10; j <= 10; j++)
                {
                    isSetMap.GetComponent<Tilemap>().SetTile(new(i, j, 0), null);
                }
            }
            if (canSetMap.GetComponent<Tilemap>().GetTile(currentCellPos) != null)
            {
                isSetMap.GetComponent<Tilemap>().SetTile(currentCellPos, tile);
            }
        }
        // Refresh tiles
        lastCellPos = currentCellPos;// Refresh position
    }

    private void SetTile()
    {
        if (isSetMap.GetComponent<Tilemap>().GetTile(currentCellPos) != null 
            && Input.GetKey(KeyCode.Mouse0))
        {
            GameObject.Find("tileNormGround").
                GetComponent<Tilemap>().SetTile(currentCellPos, tile);
        }
        if (isSetMap.GetComponent<Tilemap>().GetTile(currentCellPos) != null
            && Input.GetKey(KeyCode.Mouse1))
        {
            GameObject.Find("tileNormGround").
                GetComponent<Tilemap>().SetTile(currentCellPos, null);
        }
    }
}
