UrbanWheels
Project for CS6457

Creation of Team Mandelbrots


Start Scene: Opening Menu (UrbanWheels\Assets\Scenes\GameScenes\Opening Menu)

How to play:

1. Open the Opening Menu scene.
2. From here, you are presented with three options: 
    1. Start Driving - starts the game from where you left off. It will start from the beginning if no saved data
	-- functionality: WASD for directions, left mouse to shift gear up, right mouse to shift gear down, space bar to turn to new street. 
    2. Restart Game - starts the game from fresh. Deletes all saved data.
    3. Exit - closes the game


3. On the Game Loop scene (which is what is called from the Start Driving button), a city will be generated and loop with random turns and generating obstacles.
The goal is for the car to avoid colliding with the obstacles and complete missions by picking up passengers and delivering them to their destinations.  
During this scene, there is a pause button that can be used to pause game play. When it is opened, music will be played and options will be presented to the user
to save their current data, continue playing or exit. After saving once, the save button will be disabled unless the pause menu is opened again.
4. Upon the completion of each level (day), the End of Day scene menu will be presented. This gives the data about the user's stats and the options to:
    1. Continue to the next day (new quests)
    2. Restart from scratch with no data saved.
    3. Exit the game

Known Problem Areas:

1. Boy obstacles only spawn in the original city street. 


Manifest:
Denver Reed 
Completed all pause menu, main menu, and end of day menu functionality, including the sound associated with each menu.
Created the HUD. 
Also added the functionality to save data between runs.
Assisted with the obstacle devolpment (correcting the boy's animation and creating the cat's animation)
Added the obstacles to automatically generate within the city blocks
Imported these assets from Unity (since no credit scene exists yet):
https://assetstore.unity.com/packages/audio/music/electronic/free-soundtrack-chill-synthwave-194432 (menu music)
https://assetstore.unity.com/packages/2d/pixel-cars-178447 (main menu icon)
https://assetstore.unity.com/packages/2d/fonts/free-pixel-font-thaleah-140059 (menu font)
https://assetstore.unity.com/packages/2d/gui/icons/2d-casual-ui-hd-82080 (menu and HUD icons)

    - hudDisplay.cs
    - parts of GameManager.cs
    - DisplayStats.cs
    - AudioEventManager.cs (minus collisions. That was Sophie)
    - CloseMenu.cs
    - GameQuitter.cs
    - GameStarter.cs
    - RestartGame.cs
    - SaveGame.cs
    - TogglePauseMenu.cs
    - additions to the Animators for the Boy and cat
    - CatAnimation.cs
    - additions/modifations to KidsScript.cs
    
David Strube
Researched several kid and cat assets on Unity Asset Store, skimming thru their demo scenes to see which ones had pre-existing functionality that looked good enough
Added and imported these two:
	https://assetstore.unity.com/packages/3d/characters/kids-character-free-242192
	https://assetstore.unity.com/packages/3d/characters/animals/lowpoly-toon-cat-lite-66083
Modified KidsScript.cs to remove character control input and limit mobility to the features we want
Fixed texture of boy and cat
Worked with Denver to figure out why the city disappeared when the boy and cat were added to the City game object
Merged several pull requests
Assisted Sophie with branch creation and pull request
Recorded the Gameplay & Trailer videos

Steven Deam
Created procedural city generation and player control and turning.  
Designed City and Player character Prefabs from https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899.  
Created city block variations based on imported assets.  
Designed Turn signal arrows and functionality for speed indicator.  

Created and authored files:
  - CityBlock.cs
  - CityGenerator.cs
  - ExitLocation.cs
  - PlayerController.cs
  - TurnSpeedCheck.cs
Created and shared authorship of:
  - GameManager.cs

Jordan Esposita
Created Questing System, passenger prefabs, modified several files.  

Created and authored files:
  - MinimapController.cs
  - PassengerSpawner.cs
  - Quest.cs
  - QuestBox.cs
  - QuestCompletion.cs
  - QuestGenerator.cs
  - QuestManager.cs
  - QuestNPCHander.cs
  - QuestPathing.cs
  - QuestCollider.cs