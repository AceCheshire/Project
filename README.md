# WHO STEAL MY NUT

## Statistics          ![GitHub commit activity](https://img.shields.io/github/commit-activity/t/AceCheshire/Project?logoColor=violet&label=Commit%20Activity)     ![Github Created At](https://img.shields.io/github/created-at/AceCheshire/Project)     ![GitHub Release Date](https://img.shields.io/github/release-date/AceCheshire/Project)

## Design

**[Design Profile](https://github.com/AceCheshire/Project/blob/main/%E8%BD%AF%E4%BB%B6%E8%AE%BE%E8%AE%A1%E8%AF%B4%E6%98%8E%E4%B9%A6.md)**

## How To Use

**[Use Profile](https://github.com/AceCheshire/Project/blob/main/%E8%BD%AF%E4%BB%B6%E4%BD%BF%E7%94%A8%E8%AF%B4%E6%98%8E%E4%B9%A6.md)**

## Development Log

**Tips:** 

 **#     -     [AceCheshire](https://github.com/AceCheshire "AceCheshire")**

 **\*     -     [splendid-atom](https://github.com/splendid-atom "splendid-atom")**
     
 **&     -     [Sleepiggyy](https://github.com/Sleepiggyy "Sleepiggyy")**
    
### 2024/02/11

2024/02/11/02 #

This push interrupted several important files

Sad

2024/02/11/03 #

This push is to make up for the interruption.

However, there is still an alphaBet sprite sheet to make up.

Once we have alphaBet, we can develop a tilemap - based way to present words;

2024/02/11/04 #

New backGround has launched.

### 2024/02/12

2024/02/12/01 #

Seperate backGround and backGroundBox.

Slow down the speed of BackGroundShake.

2024/02/12/02 #

Add achievementButton;

Add StageOne scene;

Prefab tilemaps;

Add several tiles;

2024/02/12/03 #

Add title in the WelcomeScene;

Add SplashImage;

Change some object sprites so as to fit the overall bloom.

### 2024/02/13

2024/02/13/01 #

Change ProjectSetting from full screen to windowed.

2024/02/13/01 *

Add a new scene and the button to change scene.

### 2024/02/14

2024/02/14/01 #

Add a new scene and two modeButtons.

Change scene names.

Modify some scripts. ( Attention )

Change the speed of WordTitleShake.

The backGround story has not been finished yet. 

2024/02/14/02 #

Add backGroundBox family.

Change the type of selectButton.

Postpone BackGroundShake anime.

Add appear effects for all objects in Menu scene.

Add a homeButton.

Modify sortingOrders.

Now only when nut ( selectButton ) collides the bottom will scene change.

Modify the intervals of the three buttons.

2024/02/14/03 #

Change SplashImage.

Now nut need to collide twice to change scene.

### 2024/02/15

2024/02/15/01 #

Tile Update: 

Add enchanted tiles: Sync, Xyz and Link.

Usage: 

 Sync can accumulate nut.

 Xyz can split nut into two the same nuts.

 Link can change velocity direction of nut.

Add floating of tiles in the following way:

Rend every cell per frame with different tiles, which are diffrent frames of the same kind of tile.

### 2024/02/16

2024/02/16/01 #

Add WordTranslate system.

However, it hasn't been finished yet. 

Wow, my push also failed.

2024/02/16/02 #

Fix the bug that used to force objects move up and down so as to make them fluent.

2024/02/16/03 #

Important Update:

Add WordTranslate System. ( No  annotations now )

Following is brief introduction:

Take WelcomeScene as an example, There are three main tilemaps: wordAlphabet, wordBuffer and wordRenderer.

When we press down one button ( e.g. readmeButton ) , the working flow is: 

Scripts attached to the readmeButton invoke a public void of WelcomeSceneLayerConfig, 

which is called " WelcomeReadmeAlertOn() " to open the readmeAlert;

the readmeAlert includes three parts: wordBoard, wordRenderer and wordBoardExitButton.

WelcomeReadmeAlertOn() finish three tasks in a row: 

1) Modify the inner string of wordBuffer's script ( the script is called WordTranslate.cs ) ;

2) Change readmeAlert's three parts' sortingOrder to the top;

3) Send a message through parameter " isWaitingRend " to wordBuffer's script ( WordTranslate.cs );

wordBuffer and wordRenderer both are waiting messages every frame.

After WelcomeReadmeAlertOn() finish its three tasks, the next frame, wordBuffer detect the message, 

so it runs a rending task all happening on the tilemap " wordBuffer ".

But how does it do the reandering task?

Well, remember we have mentioned the inner string of wordBuffer's script?  

WordTranslate.cs divides it into char array to operate one by one.

To put every char from text information to tile set on the tilemap " wordBuffer ", 

WordTranslate.cs gets where the next char's position on the tilemap should be by detect blank rows.

Then it copied every char on the tilemap " wordAlphabet " and set tiles that form a char

on the tilemap " wordBuffer " according to that next char's position ( by calculation ) and that char's 

position on tilemap " wordAlphabet ".

After above work, we get a tilemap with chars arranged on it.

Now we need to solve the problem that the wordBoard is limited, so it can't present all chars in one board. 

Let the tilemap " wordbuffer " and " wordAlphabet " be visibly false,

we now discuss the tilemap we directly see on the screen, " wordRenderer ".

wordRenderer will receive a message from WordTranslate.cs, which is named " isWaitingFrontRend ", 

and when received, wordRenderer will make a " FrontRend ".

wordRenderer defines a Vector3Int to tell itself where to start copy, 

and FrontRend will copy a part of tilemap " wordBuffer " to the tilemap " wordRenderer ".

Where that part will be copied is apperently the area of wordBoard, 

so that copied part is exactly as big as the area of wordBoard.

To add FlipPage function, we just clean what we have FrontRend-ed, 

move our Vector3Int ( we know, it is a teller to tell wordRenderer where to start copy) 

and simply FrontRend again with the new Vector3Int. 

What I have finished is flip pages with UpArrow and DownArrow on the keyboard.

Besides, I add function to detect a passage's beginning and end so that our FlipPage will not allow you to flip 

unlimited.

To make similar WordTranslate function in different scene, we need to have the following preparation:

1) Directly copy object " wordAlphabet "," wordBuffer ",
   
   " wordBoard ", " wordBoardExitButton " and " wordRenderer ";

3) Build a script like Script " SortingWelcomeSceneObject " of WelcomeSceneLayerConfig.

   Its usage is to manage alert changes in one script.

4) Set a button that will push alerts;

5) Link the button to the alert-change-managing script ( e.g.SortingWelcomeSceneObject ),

   link the alert-change-managing script to wordBuffer, and make sure wordBuffer still links to wordRenderer.

6) Now basic links have been formed, you can change texts in your alert-change-managing script,

   or even other parameters. ( e.g. add chars to wordAlphabet )

Besides, now we need to click the glass bin to select scene.

2024/02/16/01 *

Add stageOneButton in SelectScene.

### 2024/02/17

2024/02/17/01 #

Add basic attributes for StageOne.

Add annotations for WordTranslate Scripts.

Add annotations for NormChange.cs.

Add some words to readmeAlert and achievementAlert

2024/02/17/01 *

Add AudioData for buttons.

### 2024/02/20

2024/02/20/01 #

Write ( not Add ) two buttons to switch to EnchantMode and RunningMode.

Write a solution to set temporary tiles so that player can see tile position before formal setting tiles.

Add Chinese annotations for StageOne scripts.

Rename some parameters.

Now we need to solve all the bugs to let game run. 

### 2024/02/23

2024/02/23/01 #

Add backGroundStageOne, but it is only a temporary background;

Delete all buttons' transition time;

Change words in alerts to be able to be paged up or down by mouse scrollwheel;

### 2024/02/25

2024/02/25/01 #

Fix bug that tiles cannot be correctly set.

### 2024/02/26

2024/02/26/01 #

Add repeatButton and selectStageButton.

Modify some objects' sorting orders.

Add wordTranslate system to StageOne and prepare to add alerts.

### 2024/02/27

2024/02/27/01 #

Add alerts.

### 2024/02/29

2024/02/29/01 #

Add new scene "Plot";

Add guide words for Plot;

Add example plots for Plot.

### 2024/03/01

2024/03/01/01 &

Try to add nut's falling.

Try to add nut's bouncing.

### 2024/03/02

2024/03/02/01 #

Fix some bugs after merge.

### 2024/03/03

2024/03/03/01 #

Add LoseJudege;

Add obstacles;

### 2024/03/04

2024/03/04/01 #

Add Plot;

### 2024/03/05

2024/03/05/01 #

Fix bug of EarthCreateModeButton.

Add startPoint and endPoint.

Modify the format of all words.

Add more obstacles;

Add mana calculation system.

Add Bloom effects;

Now isSetGround is half - visible.

You can only set the first tile on the startPoint.

### 2024/03/06

2024/03/06/01 #

Add GradeJudge system.

2024/03/06/02 #

Add backGround Music;

### 2024/03/07

2024/03/07/01 #

Add EnchantCreateMode;

Add guide button.

### 2024/03/08

2024/03/08 #

Add guide texts;

### 2024/03/09

2024/03/09/01 *

Add Achievement system;

2024/03/09/01 #

Add sound for nut.

### 2024/03/17

2024/03/17/01 #

Add new stage;

Fix bug that runMode cannot normally keep closed if there is no platform.

### 2024/03/24

2024/03/24/01 #

Add guideWords for tutorial;

Decrease the difficulty of the tutorial stage.

### 2024/04/03

2024/04/03/01 *

Add name System.

### 2024/04/07

2024/04/07/01 &

Add scripts for two enchant platform.

### 2024/04/08

2024/04/08/01 &

Add new stages.

2024/04/08/01 #

Add pictures for Plot.

Improve plots.

### 2024/04/09

2024/04/09/01 #

Fix some bugs.

Add animator for enchant.





