using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	Rigidbody body = null;
	public float horizontalSpeed = 8.0f;
	public float maxSpeed = 10.0f;
	public float jumpSpeed = 4.0f;
	
	public bool jumping = false;
	public bool falling = false;
	public bool jumpFinished = true;
	public float lastJumpTime = 0.0f;
	
	public Vector3 lastCollisionNormal = Vector3.up;
	public Vector3 lastCollisionPoint;
	
	SphereCollider ballCollider = null;
	Rigidbody ballBody = null;
	ConstantForce customGravity = null;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void FixedUpdate() {
		if (Mathf.Abs(lastCollisionNormal.y) > 0.05f) {
			Vector3 upscale = transform.up * 10.0f;
			Debug.DrawLine(lastCollisionPoint, lastCollisionPoint + (lastCollisionNormal * 10.0f));
			
			Vector3 newGravity = (lastCollisionNormal * -50.0f) + Physics.gravity;
			customGravity.force = newGravity;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//Check Horizontal Movement
		
		float hSpeed = jumping == true ? horizontalSpeed * 0.1f : horizontalSpeed;
		if (Input.GetAxis("Horizontal") < 0.0f) {
			if (rigidbody.velocity.x > maxSpeed - 10.0f)
				rigidbody.AddForce(new Vector3(-hSpeed / 2.0f, 0.0f, 0.0f), ForceMode.Impulse);
			else
				rigidbody.AddForce(new Vector3(-hSpeed, 0.0f, 0.0f), ForceMode.Acceleration);
		}
		else if (Input.GetAxis("Horizontal") > 0.0f) {
			if (rigidbody.velocity.x < -maxSpeed + 10.0f)
				rigidbody.AddForce(new Vector3(hSpeed / 2.0f, 0.0f, 0.0f), ForceMode.Impulse);
			else
				rigidbody.AddForce(new Vector3(hSpeed, 0.0f, 0.0f), ForceMode.Acceleration);
		}
		
		//If we're on the ground, check to see if jump was pressed
		/*if (Input.GetKeyDown(KeyCode.Space)) {
			if (OnGround() && !jumping) {
				jumpFinished = false;
				jumping = true;
				falling = false;
				
				rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
			}
		}
		else if (Input.GetKeyDown(KeyCode.Space)) { //If we're not on the ground, check to see if we need to do an extra jump
			if (OnGround() == false){
				lastJumpTime += Time.deltaTime;
				if (lastJumpTime < 0.5f) {
					body.AddForce(new Vector3(0.0f, 10.0f, 0.0f), ForceMode.Acceleration);
				}
				else {
					lastJumpTime = 0.0f;
					jumpFinished = true;
					jumping = false;
				}
			}
		}
		
		if (OnGround() && jumping == false) {
			jumpFinished = true;
			jumping = false;
			falling = false;
		}*/
		
		if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed) {
			Vector3 vel = rigidbody.velocity;
			vel.x = Mathf.Sign(vel.x) * maxSpeed;	
			rigidbody.velocity = vel;
		}	
	}
	
	bool OnGround() {
		Collider c = collider;
		
		if (lastCollisionPoint.y < -c.bounds.size.y * 0.1f)
			return true;
		
		return false;
	}
	
	void OnCollisionStay(Collision c) {
		ContactPoint cp = c.contacts[0];
		Vector3 pos = cp.point;
		
		lastCollisionPoint = pos;//transform.InverseTransformPoint(pos);
		lastCollisionNormal = cp.normal;
	}
	
	public void FullEnable() {
		ballCollider = gameObject.AddComponent<SphereCollider>();
		ballBody = gameObject.AddComponent<Rigidbody>();
		customGravity = gameObject.AddComponent<ConstantForce>();
		
		ballCollider.radius = 0.5f;
		/*ballCollider.material = new PhysicMaterial();
		ballCollider.material.staticFriction = 0.1f;
		ballCollider.material.dynamicFriction = 0.1f;
		ballCollider.material.bounceCombine = PhysicMaterialCombine.Minimum;
		ballCollider.material.frictionCombine = PhysicMaterialCombine.Minimum;*/
		
		ballBody.mass = 1.0f;
		ballBody.drag = 0.5f;
		ballBody.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		ballBody.useGravity = true;
		ballBody.WakeUp();
		
		customGravity.force = lastCollisionNormal * -50.0f;
		
		horizontalSpeed = 30.0f;
		maxSpeed = 40.0f;
		
	}
	
	public void CleanUp () {
		Destroy(customGravity);
		Destroy(ballCollider);
		Destroy(ballBody);
	}
}
