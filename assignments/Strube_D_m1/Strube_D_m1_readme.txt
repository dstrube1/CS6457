David Alan Strube - dstrube3@gatech.edu
CS6457 - Video Game Design
Milestone 1

Main scene: demo

1.) SomeDude_NoRootMotion – disable turn animation by NOT passing turn input parameter to the Animator in the script (only turning programmatically and the character should still be walking during the turn) 

In BasicControlScript:
// anim.SetFloat("velx", inputTurn); 


2.) SomeDude_RootMotion – add running animations (forward run AND turning runs) to forward blendtree and move the blend ratios around as appropriate so that the player can easily control the character. The character should continue to run while turning at full speed. 

In the Hierarchy tab, select SomeDude_RootMotion; then in the Inspector tab, select SimpleAnimationController; then in the Animator tab, open Blend Tree - Forward; then select Blend Tree, and in the Inspector tab, see Run, Run Left, and Run Right

3.) SomeDude_RootMotion – add public scalars that allow adjustment of animation speed and root motion scale (translation and rotation). Adjust the scalars slightly faster than default until you are happy with control speed and overall animation quality. The goal is for the character to have the capabilities of a super hero, but still look reasonable natural. 

In RootMotionControlScript:
    public float animationSpeed = 1f;
    public float rootMovementSpeed = 1f;
    public float rootTurnSpeed = 1f;

Then, in Unity Editor, in the Hierarchy tab, select SomeDude_RootMotion; then in the Inspector tab, set Animation Speed to 1.5, Root Movement Speed = 2, and Root Turn Speed to 10.

4.) Minion_RootMotion – Replace/modify the forward and turn animations to include some comical hopping steps. 

With Minion_RootMotion selected in the Hierarchy, I selected the Animator tab and opened Blend Tree - Forward, then selected Minion Forward, then in the Animation tab, I added a keyframe at 0.15 and set MinionModel : Rotation - Rotation.z = 10; then I added a keyframe at 0.30 and set the MinionModel : Position - Position.y = 0.5; then I added a keyframe at 0.45 and set the MinionModel : Rotation - Rotation.z = -10. I did the same for MinionForward Turn Left and MinionForward Turn Right

5.) Minion_RootMotion – Add animation events that generate minion squeaky footstep events to your forward animation. 

With Minion_RootMotion selected in the Hierarchy, I selected the Animator tab and opened Blend Tree - Forward, then selected Minion Forward, then in the Animation tab, I added an event at 0.30 with the object of minion-laugh-1. Similarly, for MinionForward Turn Left, I added an event at 0.30 with the object of minion-laugh-2; and for MinionForward Turn Right, I added an event at 0.30 with the object of minion-laugh-3

6.) SomeDude_RootMotion – Match Target and Inverse Kinematics addition for button press animation 

With SomeDude_RootMotion selected in the Hierarchy, I selected the Animator tab and clicked on MatchToButtonPress, and in the Inspector of MatchToButtonPress, I checked the checkbox that says "Foot IK"

7.) Also, update the name that appears on the HUD. The placeholder is found under the framerate overlay canvas.

In CharacterInputController:
public string Name = "David Alan Strube";

Any assignment requirements that have not been completed or don't work correctly: 
I'm not 100% sure I completed the Inverse Kinematics requirement for SomeDude_RootMotion. Not sure if I needed to use any of the IK functions in Unity (specifically "public void SetIKPosition(AvatarIKGoal goal, Vector3 goalPostion)", and where I would put or call the code). However it looks to me like it's working correctly.

External assets: none

Note, this game runs in full screen. To exit the game, press "x".
