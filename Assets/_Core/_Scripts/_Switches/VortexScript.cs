using UnityEngine;
using System.Collections;

public class VortexScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay(Collider other)
	{
		other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, 0.4f);
	}
}
