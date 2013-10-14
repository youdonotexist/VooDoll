using UnityEngine;
using System.Collections;

public class EnvironFireScript : MonoBehaviour {
	
	bool isBurning = true;
	float relightCountdown = 0.0f;
	
	Vector3 startPos;
	
	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(isBurning == false)
		{
			relightCountdown -= Time.deltaTime;
			if(relightCountdown <= 0)
			{
				transform.position = startPos;
				isBurning = true;
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{		
		if(other.name.StartsWith("Water"))
		{
			transform.position = new Vector3(-1000000, -1000000, -1000000);
			isBurning = false;
			relightCountdown = 10.0f;
		}
	}
}
