using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision CollisionObject)
	{
		Destroy(gameObject, 1);
		
		renderer.enabled = false;
		
		transform.Find("FireLight").light.enabled = false;
		transform.Find("Flame/InnerCore").particleEmitter.emit = false;
		transform.Find("Flame/OuterCore").particleEmitter.emit = false;
		transform.Find("Flame/Lightsource").light.enabled = false;
		transform.Find("Flame/Smoke").particleEmitter.emit = false;
		
		if (CollisionObject.gameObject.layer == 12)
		{
			Destroy(CollisionObject.gameObject);
		}
	}
}
