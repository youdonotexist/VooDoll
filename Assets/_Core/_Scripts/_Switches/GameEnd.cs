using UnityEngine;
using System.Collections;

public class GameEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	
	void OnTriggerEnter(Collider other)
	{
		if(other.name == "Player")
			Application.LoadLevel("Ending");
	}
}
