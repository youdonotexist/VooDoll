using UnityEngine;
using System.Collections;

public class WoodDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SwitchDeactivated() {
		gameObject.active = true;
	}
	
	void SwitchActivated() {
		gameObject.active = false;	
	}
	
	
	
}
