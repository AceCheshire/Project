using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GuideCorner : MonoBehaviour
{
    string[] guideWords = new string[30];
    public StageOneStatus status;
    public WordTranslateOneGuide wordBuffer;
    public Tilemap normGround;
    public LoseJudge judger;
    private float timer = 0f;
    private float delayer = 0f;
    private bool isPushed = false;
    public Animator enchantButton;

    // Start is called before the first frame update
    void Start()
    {
        guideWords[0] = "Click the Earth Create Button to open earth - create mode";
        guideWords[1] = "The black fork is the start point. Left Mouse to set your first platform there";
        guideWords[2] = "Well done! Now set the second platform next to the first one or a block away";
        guideWords[3] = "Set more platforms until the last platform touches the end point which is shining";
        guideWords[4] = "Now try to close earth - create mode by click the button again";
        guideWords[5] = "Click the Run Mode Button to push nut down";
        guideWords[6] = "Game Over! Click the Repeat Button to try again!";
        guideWords[7] = "Using Enchant Create Mode in this stage is not allowed";
    }

    // Update is called once per frame
    void Update()
    {
        timer += 0.01f;
        if (timer >= delayer)
        {
            isPushed = false;
            delayer += 1f;
        }
        if (enchantButton.GetBool("isHovering") && !isPushed)
        {
            wordBuffer.inputStr = guideWords[7];
            isPushed = true;
            wordBuffer.isWaitingRend = true;
        }
        else if (!status.isEarthCreateMode && !isPushed && status.posList.Count == 0)
        {
            wordBuffer.inputStr = guideWords[0];
            isPushed = true;
            wordBuffer.isWaitingRend = true;
        }
        else if (status.isEarthCreateMode && !isPushed && status.posList.Count == 0)
        {
            wordBuffer.inputStr = guideWords[1];
            isPushed = true;
            wordBuffer.isWaitingRend = true;
        }
        else if (status.isEarthCreateMode && !isPushed && status.posList.Count != 0)
        {
            if (status.posList.Count == 1 &&
                normGround.GetTile(status.endPos - new Vector3Int(1, 1, 0)) == null)
            {
                wordBuffer.inputStr = guideWords[2];
                isPushed = true;
                wordBuffer.isWaitingRend = true;
            }
            if (status.posList.Count >= 2 &&
                normGround.GetTile(status.endPos - new Vector3Int(1, 1, 0)) == null)
            {
                wordBuffer.inputStr = guideWords[3];
                isPushed = true;
                wordBuffer.isWaitingRend = true;
            }
            if (status.posList.Count >= 2 &&
                normGround.GetTile(status.endPos - new Vector3Int(1, 1, 0)) != null)
            {
                wordBuffer.inputStr = guideWords[4];
                isPushed = true;
                wordBuffer.isWaitingRend = true;
            }
        }
        else if (!status.isEarthCreateMode && !status.isRunningMode &&
            !isPushed && status.posList.Count != 0 && !judger.isJudged)
        {
            wordBuffer.inputStr = guideWords[5];
            isPushed = true;
            wordBuffer.isWaitingRend = true;
        }
        else if (judger.isJudged == true && !isPushed && status.posList != null)
        {
            wordBuffer.inputStr = guideWords[6];
            isPushed = true;
            wordBuffer.isWaitingRend = true;
        }
        else
        {
            if (!isPushed && status.runTimer <= 1.1f)
            {
                wordBuffer.inputStr = "     ";
                isPushed = true;
                wordBuffer.isWaitingRend = true;
            }
        }
    }
}
