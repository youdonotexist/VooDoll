using UnityEngine;
using System.Collections;

public class BreakableWood : MonoBehaviour
{
	public GameObject LeftBreakable = null;
	public GameObject RightBreakable = null;
	public GameObject Collidable = null;
	
	public bool destroyed = false;
	
	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnTriggerEnter(Collider c) {
		ElementPlayerController controller = c.GetComponent<ElementPlayerController>();
		CharacterMotor motor = c.GetComponent<CharacterMotor>();
		if (controller && motor) {
			if (controller.state == ElementPlayerController.DOLL_STATE.EARTH) {
				if (controller.substate == ElementPlayerController.DOLL_SUB_STATE.EARTH_HEAVY) {
					if (Mathf.Abs(motor.movement.velocity.y) > 20.0f) {
						LeftBreakable.rigidbody.isKinematic = false;
						RightBreakable.rigidbody.isKinematic = false;
						Collidable.active = false;
						
						LeftBreakable.rigidbody.AddForceAtPosition(new Vector3(0.0f, Random.Range(-4.0f, -9.0f), 0.0f), new Vector3(Random.Range(4.0f, 6.0f), 0.0f, 0.0f));
						RightBreakable.rigidbody.AddForceAtPosition(new Vector3(0.0f, Random.Range(-5.0f, -10.0f), 0.0f), new Vector3(Random.Range(3.0f, 8.0f), 0.0f, 0.0f));
					}
				}
			}
		}
		
	}
	
	void OnTriggerStay(Collider c) {
		
	}
}

