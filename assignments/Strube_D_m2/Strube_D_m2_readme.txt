David Alan Strube - dstrube3@gatech.edu
CS6457 - Video Game Design
Milestone 2

Main scene: demo

1.) Your name appears in the HUD
	This was done in Milestone 1 and required no modification.

2.) Vertical stack of three (3) blue rigidbody spheres with collision sounds
	In Hierarchy, there is a folder called BlueSpheres. In that folder, there are 3 spheres: BlueSphere1-3, each with a Bomb Bounce Reporter set to BombBounceReporter and a Blue material. The BombBounceReporter is like the CrateCollisionReporter except it has only 2 parameters instead of 3.

3.) Vertical stack of three (3) red rigidbody spheres that don’t collide with one
another.
	In Hierarchy, there is a folder called RedSpheres. In that folder, there are 3 spheres: RedSphere1-3, each with a Red material, and each assigned to the Layer "Red". There is no Bomb Bounce Reporter. To set the red spheres to not interact with each other, I went to File -> Build Settings -> Player Settings -> Physics, and unchecked the Red <-> Red checkbox.

4.) Asset Store model “Free Japanese Mask” with custom compound collider that tips
over on nose similar to provided video link above. (DO NOT USE A MESH COLLIDER FOR THIS TASK.)
	After importing the Kitsune asset from the asset store, I adjusted its position and rotation to be facing the players with its nose near the ground. I also attached a capsule where the nose is, a couple cylinders where the ears are, and a cube where the face is, adjusted their scaling to fit near or within the mask, and turned off their Mesh Renderers to make them invisible.

5.) yellow jointed chain made of at least five (5) rigidbody GameObjects
	I added a folder called Chain. Under that, I added 5 spheres: ChainSphere1-5. For each ChainSphere, I added a Yellow material, a rigid body, and a Configurable Joint with X Motion, Y Motion and Z Motion set to Locked. For ChainSphere2, I set the Connected Body = ChainSphere1; for ChainSphere3, I set the Connected Body = ChainSphere2; and so on. I positioned all the ChainSpheres so that when the game starts, the chain is swinging.

6.) blue kinematic rigidbody elevator using Mecanim animation with red rigidbody
sphere going for a ride
	For this, I followed the instructions in the Milestone 2 document. This was very straightforward.

7.) green Weeble Wobble/Punching Bozo that tilts but can’t be knocked over
	I added a Capsule named WeebleWobble, assigned it a green material, and added a Rigid Body Center of Mass with the Y=-1, and the rotation of the WeebleWobble such that it started off tilted so that it would be wobbling as soon as the game started.

8.) a purple cube with rigidbody box that does not slide down the ramp
	I added a cube named PurpleBox, set its material to a purple material I added, and set its position and rotation such that it was positioned on the ramp. I then set its Mass, Drag, and Angular Drag to an insanely large number (1e+07) so that it would be unlikely to move anytime soon.

9.) green cube with rigidbody box that does slide down the ramp
	I added a cube named GreenBox, set its material to a green material I added, and set its position and rotation such that it was positioned on the ramp. I then created a new physics material under the physics_materials folder in the Hierarchy. This new physics material is named Slidey, and (as specified in the Milestone 2 document) has Dynamic & Static Frictions of 0.1, and Friction Combine = Minimum. I then set GreenBox's Box Collider Material = Slidey.

10.) orange rigidbody sphere that bounces
	In Hierarchy, I added a sphere named OrangeSphere. I set its material to an orange material I added named Orange. I then created a new physics (similar to the one I created for Slidey), named Bouncy. Bouncy has Dynamic & Static Frictions of 0.6, Bounciness of 0.7, and Bounce Combine = Maximum. I then set OrangeSphere's Sphere Collider Material = Bouncy. I set OrangeSphere's Y = 5 so that it would start off high and bounce when the game started.

11.) Y_Bot ragdoll that collapses over the hurdle GameObject (it’s ok for ragdoll to fall
off)
	In the Project tab, under Assets -> ModelsAndAnimations -> Ybot with Animations -> Y_Bot, I clicked and dragged Y_bot to the Hierarchy tab and renamed it to RaggedyMan. Next under RaggedyMan, I renamed mixamorig:Hips to RM_mixamorig:Hips, and similarly renamed many other components from [x] to RM_[x]. I did this so that the components for this game object would be distinguishable from all the others when I assigned these components to the slots in the Create Ragdoll dialog; e.g., RM_mixamorig:Hips -> Pelvis. I also checked the checkbox for Flip Forward.

12.) black click beetle or jumping bean that jumps intermittently and is autonomous
(not controlled by the player). There should be a variable/random amount of time between jumps and variable/random force magnitude and direction. Jumps only happen when grounded.
	In Hierarchy, I added a capsule named JumpingBean, assigned the black material to it, and scale and rotation so that it started off laying down in the sight of the players. I then added a rigid body and a new script called JumpingBeanControlScript. In JumpingBeanControlScript, I added a Rigidbody object named rbody and initialized a System.Random object. The rbody was initialized in Start; and in FixedUpdate, I created three ints - moveDie, forceDie, directionDie - that would randomly determine (respectively) whether or not to move (about 30% of the time), how forcefully to move with Impulse, and in which direction to move. If the direction chosen was up, then the force modifier would be doubled, to give more of a jumping behavior.

13.) Make a pause script that starts the game paused and unpauses with “p” keypress
	In CharacterInputController, in Update(), at the end of the chain of if / else ifs that handle the key inputs to adjust the player speed, I added a clause to handle the key P, and if it's pressed, toggle between Time.timeScale = 1 & Time.timeScale = 0;

Any assignment requirements that have not been completed or don't work correctly: 
	When SomeDude_RootMotion or Minion_RootMotion (the two players whose motion I modified for Milestone 1) step onto the elevator, they're not able to move anymore.

External assets: 
	Kitsune / Japanese Mask

Note, this game runs in full screen. To exit the game, press "x".
