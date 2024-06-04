David Alan Strube - dstrube3@gatech.edu
CS6457 - Video Game Design
Milestone 3

Main scene: GameMenuScene
Additional scenes: demo

1- The M2 pause script is not active in the scenes.

2- The Game Start Menu has a dedicated scene - GameMenuScene
	There is an Overlay UI Panel (Start Menu) menu panel that is centered in the screen and does not completely fill the screen. It has a Start Game button & an Exit button. The post-processing effect on the camera background include: 
	- Anti-aliasing with fast mode and Mode = Fast Approximate Anti-aliasing (FXAA)
	- Deferred fog enabled and skybox excluded
	- Global Post-process Volume
	- Color grading on with Mode=Low Definition Range
	- White balance temperature = 44 & tint = 11
	- Color filter = HDR
	- Brightness = -30
	- Contrast = -44
	- Grain turned on with "Colored" enabled, Intensity = 1, Size=3, and Luminance Contribution=1
I toyed around with other settings, but these seemed sufficient for the background effects. I also disabled most background action for this scene, leaving only the weeble wobble, yellow chain, black jumping bean, and green box sliding with low friction active
	

3- There is an In-Game Menu in the original gameplay scene. 
The overlayed UI Panel is centered on the screen and does not completely fill the screen. There is a Restart Game button & an Exit button. The panel toggles on/off with Escape key. The game pauses when this menu is enabled.

4- There is a pink collectable ball that only SomeDude_RootMotion can collect from the top of the ramp in front of him. It has a radius of 3.14.

5- There are trigger-based animated prefab object placed in three (3) locations in scene that only respond to SomeDude_RootMotion. The objects are prefabbed. They're animated via Mecanim in a compelling way when SomeDude_RootMotion gets close enough, and they reset when he is far enough away. They have a radius of 3.14. Their locations (xyz coordinates) are: 
	1: 0, 1, 0
	2: 6, 1, -14
	3: -3, 1, 12

The game build starts from the start menu scene 

Any assignment requirements that have not been completed or don't work correctly: 
	Only the extra credit.

External assets: 
	Kitsune / Japanese Mask (from milestone 2)

Note, this game runs in full screen. To exit the game, press "x", or click the "Exit" button before game play, or press Escape and then click the "Quit Game" button during game play.
