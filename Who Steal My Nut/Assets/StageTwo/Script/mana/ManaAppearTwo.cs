using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ManaAppearTwo : MonoBehaviour
{
    public StageTwoStatus status;
    public GameObject wordRenderer;
    public GameObject wordBuffer;
    public int totalMana = 0;
    private int enchantMana = 150;
    private int earthMana = 100;

    private string manaWord;

    private float timer = 0f;
    private float delayer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        totalMana = status.enchantList.Count * 150 + status.posList.Count * 100;
        if (status.isEarthCreateMode == true || status.isEnchantCreateMode == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 30;
            wordRenderer.GetComponent<TilemapRenderer>().sortingOrder = 30;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            wordRenderer.GetComponent<TilemapRenderer>().sortingOrder = -1;
        }
        if (timer >= delayer)
        {
            if (status.isEnchantCreateMode == true)
            {
                manaWord = totalMana + " + " + enchantMana;
            }
            if (status.isEarthCreateMode == true)
            {
                manaWord = totalMana + " + " + earthMana;
            }
            wordBuffer.GetComponent<WordTranslateTwoNumTwo>().inputStr = manaWord;
            wordBuffer.GetComponent<WordTranslateTwoNumTwo>().isWaitingRend = true;
            delayer += 1f;
        }
    }
}
