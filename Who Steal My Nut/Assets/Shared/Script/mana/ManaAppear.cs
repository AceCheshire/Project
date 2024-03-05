using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ManaAppear : MonoBehaviour
{
    public StageOneStatus status;
    public GameObject wordRenderer;
    public GameObject wordBuffer;
    public int totalMana = 0;
    private int enchantMana = 300;
    private int earthMana = 100;

    private string manaWord;

    private float timer = 0f;
    private float delayer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        totalMana = status.enchantList.Count * 300 + status.posList.Count * 100;
        if (status.isEarthCreateMode == true || status.isRunningMode == true)
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
            wordBuffer.GetComponent<WordTranslateOneNumTwo>().inputStr = manaWord;
            wordBuffer.GetComponent<WordTranslateOneNumTwo>().isWaitingRend = true;
            delayer += 1f;
        }
    }
}
