using UnityEngine;
using System.Collections;

//Attach the Switch Effect component to the object you want to change when the switch is activated

public class HeavyButton : MonoBehaviour {
	public string activateMethodName;
	public string deactivateMethodName;
	
	public GameObject switchEffector;
	
	public bool oneTimeSwitch = false;
	public bool activated = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider c) {
		ElementPlayerController controller = c.GetComponent<ElementPlayerController>();
		if (controller != null) {
			if (controller.substate == ElementPlayerController.DOLL_SUB_STATE.EARTH_HEAVY) {
				if (switchEffector != null && activateMethodName != null) {
					activated = true;
					switchEffector.SendMessage(activateMethodName);
				}
			}
		}
	}
	
	public void OnTriggerStay (Collider c) {
		ElementPlayerController controller = c.GetComponent<ElementPlayerController>();
		if (controller != null) {
			if (controller.substate == ElementPlayerController.DOLL_SUB_STATE.EARTH_HEAVY) {
				if (switchEffector != null && activateMethodName != null && activated == false) {
					activated = true;
					switchEffector.SendMessage(activateMethodName);
				}
			}
			else {
				if (switchEffector != null && deactivateMethodName != null && oneTimeSwitch == false) {
					activated = false;
					switchEffector.SendMessage(deactivateMethodName);
				}
			}
		}
	}
			         
	public void OnTriggerExit(Collider c) {
		ElementPlayerController controller = c.GetComponent<ElementPlayerController>();
		if (controller != null) {
			if (controller.substate == ElementPlayerController.DOLL_SUB_STATE.EARTH_HEAVY) {
				if (oneTimeSwitch == false) {
					if (switchEffector != null && deactivateMethodName != null) {
						activated = false;
						switchEffector.SendMessage(deactivateMethodName);
					}
				}
			}
		}
	}
	
	public void Activate() {
		Vector3 pos = transform.position;
		pos.y -= 4.0f;
		transform.position = pos;
	}
	
	public void Deactivate() {
		Vector3 pos = transform.position;
		pos.y += 4.0f;
		transform.position = pos;
	}
}
