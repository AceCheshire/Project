using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

/*### �ⶫ����ȫ���в����� ###*/
/*### ���⣬Ϊɶ play ֮�� nut �������� (0,0,0) �� ###*/
/*### vocal ��play ֮����Щû����ȥ�� tile Ϊ���ֳ����� ###*/
/*### ������д���з��� ###*/
public class StageOneStatus : MonoBehaviour
{
    /*Lock & Unlock*/
    public bool isEarthCreateMode = false;
    public bool isEnchantCreateMode = false;
    public bool isRunningMode = false;
    
    

    /*Timer*/
    private float timer = 0f;
    private float delayer = 0f;

    /*Unital Grid*/
    public Rigidbody2D NutRigidbody;
    public GameObject NutObject;
    public SpriteRenderer NutRenderer;
    public Tilemap tileNormGround;
    public List<Vector3Int> posList;
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayer)
        {
            delayer += Time.deltaTime;
        }
    }

    

}