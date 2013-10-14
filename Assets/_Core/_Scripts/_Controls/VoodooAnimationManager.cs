using UnityEngine;
using System.Collections;

public class VoodooAnimationManager : MonoBehaviour
{
	public PlayerControl playerControl = null;
	public CharacterMotor motorControl = null;
	public ElementPlayerController elementControl = null;
	
	public PackedSprite _packedSprite;
	
	string normal_prefix = "normal";
	string fire_prexfix = "fire";
	string earth_prefix = "earth";
	string water_prefix = "water";
	string air_prefix = "air";
	
	string direction_mod_left = "left";
	string direction_mod_right = "right";
	
	string walk_action = "walk";
	string run_action = "run";
	string jump_action = "jump";
	string crouch_action = "crouch";
	string stand_action = "stand";
	string dying_action = "dying";
	
	// Use this for initialization
	void Start ()
	{
		UpdateReferences();
	}

	// Update is called once per frame
	void Update ()
	{
		
		UpdateAnimation();
		
	}
	
	public void UpdateAnimation() {
		////
		// Select the Animation
		////
		
		//Get Current Doll Element
		ElementPlayerController.DOLL_STATE currentElement = elementControl.state;
		
		//Get Current Doll Subelement
		ElementPlayerController.DOLL_SUB_STATE currentSubElement = elementControl.substate;
		
		//Get Hz/Vt Direction
		float horizontal_movement = 0.0f;
		float vertical_movement = 0.0f;
		
		if (motorControl != null) {
			horizontal_movement = motorControl.movement.velocity.x;	
			vertical_movement = motorControl.movement.velocity.y;
		}
		else if (playerControl != null) {
			horizontal_movement = playerControl.rigidbody.velocity.x;	
			vertical_movement = playerControl.rigidbody.velocity.y;	
		}
		
		string anim_name = BuildAnimationName(currentElement, currentSubElement, horizontal_movement, vertical_movement);
		UVAnimation anim = _packedSprite.GetAnim(anim_name);
		if (anim == null) {
			anim = _packedSprite.GetAnim("normal_stand");
		}
		
		
		_packedSprite.DoAnim(anim);
		
		if (anim_name.Equals("normal_run")) {
			anim.framerate = Mathf.Max(10, (motorControl.movement.velocity.magnitude / motorControl.movement.maxSidewaysSpeed) * 20.0f);
		}
		
		////
		// Alter the animation depending on facing direction
		////
		
		Material mat = renderer.material;
		if (Mathf.Abs(horizontal_movement) > 0.0f)
			if (anim_name.Equals("earth_ball") == false)
				transform.localScale = new Vector3(Mathf.Sign(horizontal_movement), 1.0f, 1.0f);
	}
	
	string BuildAnimationName(ElementPlayerController.DOLL_STATE currentElement, ElementPlayerController.DOLL_SUB_STATE currentSubElement, float hz_mv, float vt_mv){
		string builder = "";
		
		bool onGround = true;
		if (motorControl != null)
			onGround = motorControl.controller.isGrounded;
		
		if (elementControl.dying) {
			if (elementControl.lastKilledBy.Equals("water") || elementControl.lastKilledBy.Equals("fire") || elementControl.lastKilledBy.Equals("air"))
				builder = "normal_dying";
			else
				builder = "normal_fall";
			
			Debug.Log(builder);
			
			return builder;
		}
		else {
			if (currentElement == ElementPlayerController.DOLL_STATE.NORMAL)
				builder += normal_prefix + "_";
			else if (currentElement == ElementPlayerController.DOLL_STATE.FIRE) 
				builder += normal_prefix + "_";
			else if (currentElement == ElementPlayerController.DOLL_STATE.EARTH) {
				builder += normal_prefix + "_";
				if (currentSubElement == ElementPlayerController.DOLL_SUB_STATE.EARTH_BALL) {
					builder = "earth_ball";
					Debug.Log(builder.ToString());
					return builder;
				}
				else if (currentSubElement == ElementPlayerController.DOLL_SUB_STATE.EARTH_HEAVY) {
					builder = "earth_heavy";	
					Debug.Log(builder.ToString());
					return builder;
				}
			}
			else if (currentElement == ElementPlayerController.DOLL_STATE.AIR) 
				builder += normal_prefix + "_";
			else if (currentElement == ElementPlayerController.DOLL_STATE.WATER)
				builder += normal_prefix + "_";
			
			if (onGround) {
				if (Mathf.Abs(hz_mv) < 0.01f) {
					builder += stand_action;
				}
				else {
					builder += run_action;	
				}
			}
			else {
				builder += jump_action;	
			}
		}
		
		
		
		return builder;
		
	}
	
	public void UpdateReferences() {
		playerControl = GetComponent<PlayerControl>();
		motorControl = GetComponent<CharacterMotor>();
		elementControl = GetComponent<ElementPlayerController>();
	}
	
	public void RunAnimation(string name) {
		_packedSprite.DoAnim(name);	
	}
	
}

