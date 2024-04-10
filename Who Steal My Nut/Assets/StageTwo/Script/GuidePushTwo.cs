using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePushTwo : MonoBehaviour
{
    public SortStageTwoObject layerController;
    public WordTranslateTwo wordBuffer;
    private string[] guideText = new string[20];
    private int pageCount = 11;
    private int currentPage = 0;
    private float timer = 0f;
    private float delayer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        guideText[1] = "1 Button\n\n" +
            "1.1 Mode Button\n\n" +
            "Platform Setting Mode\n" +
            "open and close platform setting mode.\n\n" +
            "Enchant Mode\n" +
            "open and close enchant mode.\n\n" +
            "Run Mode\n" +
            "open and close run mode.\n\n";
        guideText[2] = "1.2 Menu Button\n\n" +
            "Home Button\n" +
            "click it to go back to title scene.\n\n" +
            "Menu Button\n" +
            "click it to go back to the scene to select stages.\n\n" +
            "Repeat Button\n" +
            "click it to restart this stage.\n\n" +
            "Guide Button\n" +
            "click it to see guidelines for the game.\n\n";
        guideText[3] = "2 Platform Setting Mode\n\n" +
            "2.1 Preview\n\n" +
            "if you open platform setting mode, " +
            "you can see \nwhere you will set your platform before you really \nset it. " +
            "it will follow your mouse.\n\n" +
            "2.2 Set Platform\n\n" +
            "when you can preview your platform, click your left mouse button, " +
            "and you can figure out your newly \nset platform.\n\n";
        guideText[4] = "2.3 Delete Platform\n\n" +
            "when you can preview your platform, click your \nright mouse button, " +
            "and you can figure out your \nnewly deleted platform. \n\n" +
            "2.4 Platform\n\nMana Cost -- 100\nBounce Ability -- Normal\n" +
            "Bounce Speed -- Equal\nSpecial Attribute -- Waiting to be found\n\n";
        guideText[5] = "3 Enchant Mode\n\n3.1 How to Enchant\n\n" +
            "if you open enchant mode, you can see where you \nwill set your platform before you really set it. " +
            "it will \nfollow your mouse.\n\nyou can only enchant on normal platform.\n\n" +
            "when you can preview your enchanted platform, \nclick your left mouse button, " +
            "and you can figure out your newly enchanted platform.\n\n";
        guideText[6] = "3.2 How to Cancel Enchant\n\n" +
            "when you can preview your enchanted platform, \nclick your right mouse button, " +
            "and you can figure \nout your newly deleted enchanted platform.\n\n";
        guideText[7] = "3.3 Enchanted Platform\n\nMana Cost -- 150 / 125 / 175\n" +
            "Bounce Ability -- Far / Normal / Normal\nBounce Speed -- Quick / Equal / Equal\n" +
            "Special Attribute -- Teleport between two white \nplatforms / Add a shield for your nut / " +
            "Destroy \nsurrounding obstacles\n\n" +
            "Before 4 How To Move Map\n\nTry WASD or the four Arrow Keys.\n\n";
        guideText[8] = "4 Map\n\n4.1 Start Point\n\n" +
            "you must set your first platform at  the start point.\n\n" +
            "4.2 End Point\n\n" +
            "your goal is to move the nut to the end point.\n\n" +
            "4.3 Obstacle - Bleak Magic Crystal\n\n" +
            "you should not let the nut hit those crystal.\n\n";
        guideText[9] = "5 Run Mode\n\n" +
            "5.1 Overview\n\n" +
            "after you click this button, nut will appear and try to move according to the platforms you set." +
            "remember that if you set a platform after another, " +
            "the nut will give priority to the earlier set platform.\n\n";
        guideText[10] = "5.2 Failure Condition\n\n" +
            "1) Hit Bleak Magic Crystals.\n\n" +
            "2) Falling Since The Next Platform Is Too Far Away.\n\n" +
            "3) Failed To Reach The End Point.\n\n5.3 Success Condition\n\n" +
            "Get To The End Point Without Hitting Or Falling.\n\n";
        guideText[11] = "6 Achievement\n\n6.1 Statistics\n\n" +
            "every time you successfully get the nut to the end \npoint, " +
            "you can see your statistics over that game. " +
            "\npay attention to your grade. it can turn out to be A, B or C. " +
            "current that grade is judged fully by the \nmana you spend. \n\n" +
            "6.2 Check Your Achievement\n\n" +
            "frequently check it and enjoy this game! \n\n";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (layerController.isGuide) PushText();
        else currentPage = 0;
    }

    private void PushText()
    {
        if (timer >= delayer)
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                //Debug.Log("Pressed!");
                //Debug.Log(currentPage < pageCount);
                if (currentPage < pageCount)
                {
                    currentPage++;
                    wordBuffer.inputStr = guideText[currentPage];
                    //Debug.Log(guideText[currentPage]);
                    wordBuffer.isWaitingRend = true;
                }
                delayer = timer + 0.2f;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                //Debug.Log("Pressed!");
                //Debug.Log(currentPage > 1);
                if (currentPage > 1)
                {
                    currentPage--;
                    wordBuffer.inputStr = guideText[currentPage];
                    //Debug.Log(guideText[currentPage]);
                    wordBuffer.isWaitingRend = true;
                }
                delayer = timer + 0.2f;
            }
        }
    }
}
