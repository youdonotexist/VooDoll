using UnityEngine;
using System.Collections;

public class FallingBoulderTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter (Collider ColliderObject)
	{
		if (ColliderObject.gameObject.name == "Player")
		{
			transform.FindChild("Boulder").rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
			transform.FindChild("Boulder").rigidbody.WakeUp();
			transform.FindChild("Boulder").rigidbody.isKinematic = false;
		}
	}
}
