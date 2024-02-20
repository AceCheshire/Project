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
    private Vector3Int currentCellPos;
    private Vector3Int lastCellPos;

    // Start is called before the first frame update
    void Start()
    {
        sourcemap = GameObject.Find("tileFloatGround");
        canSetMap = GameObject.Find("tileCanSetGround");
        isSetMap = GameObject.Find("tileIsSetGround");
        tile = sourcemap.GetComponent<Tilemap>().GetTile(new Vector3Int(-3, -6, 0));// Reference
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
        currentCellPos = isSetMap.GetComponent<Tilemap>().WorldToCell(mousePos);
        // </GetPosition>
        if (!isTriggered)
        {
            if (canSetMap.GetComponent<Tilemap>().GetTile(currentCellPos) != null)
                isSetMap.GetComponent<Tilemap>().SetTile(currentCellPos, tile);
            isTriggered = true;
        }
        if (currentCellPos != lastCellPos)
        {
            if (canSetMap.GetComponent<Tilemap>().GetTile(currentCellPos) != null)
                isSetMap.GetComponent<Tilemap>().SetTile(currentCellPos, tile);
            isSetMap.GetComponent<Tilemap>().SetTile(lastCellPos, null);
        }
        // Refresh tiles
        lastCellPos = currentCellPos;// Refresh position
    }

    private void SetTile()
    {
        if (isSetMap.GetComponent<Tilemap>().GetTile(currentCellPos) != null 
            && Input.GetKey(KeyCode.Mouse0))
        {
            GameObject.Find("tileNormGround").GetComponent<Tilemap>().SetTile(currentCellPos, tile);
        }
    }
}
