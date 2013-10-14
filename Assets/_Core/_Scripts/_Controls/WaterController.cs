using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter (Collision CollisionObject)
	{
		transform.Find("Water Fountain").particleEmitter.emit = false;
		
		Destroy(gameObject, 1);
	}
	
	void OnTriggerEnter (Collider ColliderObject)
	{
		transform.Find("Water Fountain").particleEmitter.emit = false;
		
		Destroy(gameObject, 1);
	}
}
