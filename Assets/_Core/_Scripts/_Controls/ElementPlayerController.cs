using UnityEngine;
using System.Collections;

public class ElementPlayerController : MonoBehaviour {
	
	public float FireballCooldown = 1;
	
	public enum DOLL_STATE {
		NORMAL,
		FIRE,
		WATER,
		AIR,
		EARTH
	}
	
	public enum DOLL_SUB_STATE {
		NONE,
		EARTH_HEAVY,
		EARTH_BALL,
	}
	
	public DOLL_STATE state = ElementPlayerController.DOLL_STATE.NORMAL;
	public DOLL_SUB_STATE substate = ElementPlayerController.DOLL_SUB_STATE.NONE;
	
	public GameObject WaterPrefab;
	public GameObject FirePrefab;
	
	public bool controlsOn = true;
	public bool dying = false;
	
	public GameObject AirCloud;
	
	private CharacterMotor motor;
	private PlayerControl ballControl;
	private VoodooAnimationManager animationManager;
	
	private float timeUntilCooldown;
	private float airHoverTime = 0.0f;
	public float maxHoverTime = 2.0f;
	
	public string lastKilledBy = "water";
	
	// Use this for initialization
	void Start () {
		motor = GetComponent<CharacterMotor>();
		ballControl = GetComponent<PlayerControl>();
		timeUntilCooldown = FireballCooldown;
		animationManager = GetComponent<VoodooAnimationManager>();
		
		
		AirCloud = GameObject.Find("AirCloud").gameObject;
		AirCloud.particleEmitter.emit = false;
		ballControl.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (motor != null)
			motor.canControl = controlsOn;
		
		timeUntilCooldown -= Time.deltaTime;
		if (timeUntilCooldown < 0)
			timeUntilCooldown = 0;
		
		if (lastKilledBy == "water")
		{
			state = ElementPlayerController.DOLL_STATE.WATER;
			substate = ElementPlayerController.DOLL_SUB_STATE.NONE;
		}
		
		if (lastKilledBy == "fire")
		{
			state = ElementPlayerController.DOLL_STATE.FIRE;
			substate = ElementPlayerController.DOLL_SUB_STATE.NONE;
		}
		
		if (lastKilledBy == "earth")
		{
			state = ElementPlayerController.DOLL_STATE.EARTH;
		}
		
		if (lastKilledBy == "air")
		{
			state = ElementPlayerController.DOLL_STATE.AIR;
			substate = ElementPlayerController.DOLL_SUB_STATE.NONE;
		}
		
		if (lastKilledBy == "normal")
		{
			state = ElementPlayerController.DOLL_STATE.NORMAL;
			substate = ElementPlayerController.DOLL_SUB_STATE.NONE;
		}
		
		if(controlsOn)
			{		
			if (state == ElementPlayerController.DOLL_STATE.WATER)
			{
				if (Input.GetMouseButton(0)) {
					GameObject water = (GameObject)Instantiate(WaterPrefab, transform.position, transform.rotation);
					water.rigidbody.AddForce(-(transform.position - Camera.mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0)).normalized * Random.Range(20, 30) + motor.movement.velocity, ForceMode.Impulse);
				}
			}
			
			else if (state == ElementPlayerController.DOLL_STATE.EARTH) {
				if (Input.GetMouseButtonDown(1)) { //Ball mode (should we hold down, or click once to activate/deactiveate?)
					if (substate == ElementPlayerController.DOLL_SUB_STATE.NONE) {
						
						substate = ElementPlayerController.DOLL_SUB_STATE.EARTH_BALL;
						BallMode(true);
					}
					else if (substate == ElementPlayerController.DOLL_SUB_STATE.EARTH_BALL) {
						
						substate = ElementPlayerController.DOLL_SUB_STATE.NONE;
						BallMode(false);
					}
					//Animation changes
					//Switch out character motor?
				}
				else if (Input.GetMouseButtonDown(0)) { //Heavy Mode
					if (substate == ElementPlayerController.DOLL_SUB_STATE.NONE) {
						substate = ElementPlayerController.DOLL_SUB_STATE.EARTH_HEAVY;
						
						HeavyMode(true);
						StartCoroutine(FinishHeavyDrop());
					}
					else if (substate == ElementPlayerController.DOLL_SUB_STATE.EARTH_HEAVY) {
						//HeavyMode(false);
						
						
					}
				}
			}
			
			else if ( state == ElementPlayerController.DOLL_STATE.FIRE)
			{
				if (Input.GetMouseButton(0) && timeUntilCooldown == 0)
				{
					GameObject fire = (GameObject)Instantiate(FirePrefab, transform.position, transform.rotation);
					fire.rigidbody.AddForce(-(transform.position - Camera.mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0)).normalized * Random.Range(40, 45), ForceMode.Impulse);
					timeUntilCooldown = FireballCooldown;
				}
			}
			else if (state == ElementPlayerController.DOLL_STATE.AIR) {
			
				if (Input.GetMouseButtonDown(0)) {
					//Debug.Log("Yes");
					motor.movement.gravity = 3.0f;
					
					Vector3 move = motor.movement.velocity;
					move.y = 0.0f;
					motor.movement.velocity = move;
					motor.sliding.sidewaysControl = 0.0f;
					
					AirCloud.particleEmitter.emit = true;
				}
				else if (Input.GetMouseButtonUp(0)) {
					motor.movement.gravity = 20.0f;
					airHoverTime = 0.0f;
					motor.sliding.sidewaysControl = 0.5f;
					AirCloud.particleEmitter.emit = false;
				}
				else if (Input.GetMouseButton(0)) {
					airHoverTime += Time.deltaTime;
					if (airHoverTime > maxHoverTime) {
						motor.movement.gravity = 20.0f;
						airHoverTime = 0.0f;	
						AirCloud.particleEmitter.emit = false;
					}
				}
			}
		}
	}
	
	void Respawn()
	{
		if(controlsOn)
		{
			controlsOn = false;
		
			StartCoroutine("FinishRespawn");
		}
	}
	
	IEnumerator FinishRespawn()
	{
		dying = true;
		animationManager.UpdateReferences();
		
		yield return new WaitForSeconds(2.0f);
		
		transform.position = GameObject.Find("Start").transform.position;
		GetComponent<CharacterMotor>().movement.velocity = Vector3.zero;
		RenderSettings.ambientLight = new Color(0.2f,0.2f,0.2f);
		Camera.main.backgroundColor = new Color(0.2f,0.3f,0.475f);
		
		controlsOn = true;
		dying = false;
	}
	
	IEnumerator FinishHeavyDrop() {
		yield return new WaitForSeconds(1.0f);
		
		substate = ElementPlayerController.DOLL_SUB_STATE.NONE;
		HeavyMode(false);
	}
	
	
	void OnTriggerEnter(Collider ColliderObject)
	{
		if (ColliderObject.gameObject.tag == "EnvironFire")
		{
			lastKilledBy = "fire";
			transform.Find("Water Fountain").particleEmitter.emit = false;
			transform.Find("InnerCore").particleEmitter.emit = true;
			transform.Find("Lightsource").light.enabled = true;
			transform.Find("OuterCore").particleEmitter.emit = true;
			transform.Find("Smoke").particleEmitter.emit = true;
			
			Respawn();
		}
		
		if (ColliderObject.gameObject.tag == "Pool")
		{
			lastKilledBy = "water";
			transform.Find("Water Fountain").particleEmitter.emit = true;
			transform.Find("InnerCore").particleEmitter.emit = false;
			transform.Find("Lightsource").light.enabled = false;
			transform.Find("OuterCore").particleEmitter.emit = false;
			transform.Find("Smoke").particleEmitter.emit = false;
			
			Respawn();
		}
		
		if(ColliderObject.gameObject.tag == "EnvironVortex")
		{
			lastKilledBy = "air";
			transform.Find("Water Fountain").particleEmitter.emit = false;
			transform.Find("InnerCore").particleEmitter.emit = false;
			transform.Find("Lightsource").light.enabled = false;
			transform.Find("OuterCore").particleEmitter.emit = false;
			transform.Find("Smoke").particleEmitter.emit = false;			
			
			Respawn();
		}
		
		
		if(ColliderObject.gameObject.tag == "Boulder")
		{
			lastKilledBy = "earth";
			transform.Find("Water Fountain").particleEmitter.emit = false;
			transform.Find("InnerCore").particleEmitter.emit = false;
			transform.Find("Lightsource").light.enabled = false;
			transform.Find("OuterCore").particleEmitter.emit = false;
			transform.Find("Smoke").particleEmitter.emit = false;			
			
			Respawn();
		}
	}
	
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		/*
		Debug.Log(hit.gameObject.name);
		if (hit.gameObject.name.StartsWith("Boulder"))
		{
			lastKilledBy = "earth";
			
			Respawn();
		}*/
	}
	
		void BallMode (bool enable) {
		if (enable) {
			//CharacterController controller = GetComponent<CharacterController>();
					
			//controller.height = 1.0f;
			//motor.movement.maxSidewaysSpeed = 20.0f;
			CharacterController cc = motor.controller;
			Destroy(motor);
			Destroy(cc);
			//motor.enabled = false;
			//motor.controller.enabled = false;
			ballControl.enabled = true;
			ballControl.FullEnable();
			animationManager.UpdateReferences();
		}
		else {
			//CharacterController controller = GetComponent<CharacterController>();
					
			//controller.height = 2.0f;
			//motor.movement.maxSidewaysSpeed = 10.0f;
			
			motor = gameObject.GetComponent<CharacterMotor>();
			if (motor == null)
				motor = gameObject.AddComponent<CharacterMotor>();
			
			motor.enabled = true;
			motor.controller.enabled = true;
			motor.movement.maxForwardSpeed = 10.0f;
			motor.movement.maxSidewaysSpeed = 10.0f;
			motor.movement.maxBackwardsSpeed = 10.0f;
			
			motor.controller.height = 2.0f;
			motor.controller.radius = 0.5f;
			
			ballControl.CleanUp();
			ballControl.enabled = false;
			
			Vector3 pos = transform.position;
			pos.y += 1.0f;
			transform.position = pos;
			
			animationManager.UpdateReferences();
		}
		
	}
	
	void HeavyMode (bool enable) {
		if (enable) {
			//Up the Gravity
			//Turn off any limits
			//Negate x-based velocity
			//Turn off movement control
			motor.movement.gravity = motor.movement.gravity * 50.0f;
			motor.movement.maxFallSpeed = 1000000.0f;
			motor.controller.height = 1.0f;
			
			Vector3 vel = motor.movement.velocity;
			vel.x = 0.0f;
			motor.movement.velocity = vel;
			
			controlsOn = false;
		}
		else {
			motor.movement.gravity = 9.81f;
			motor.movement.maxFallSpeed = 20.0f;
			motor.controller.height = 2.0f;
			motor.controller.radius = 0.5f;
			
			Vector3 pos = transform.position;
			pos.y += 1.0f;
			transform.position = pos;
			
			controlsOn = true;
		}
	}
}
