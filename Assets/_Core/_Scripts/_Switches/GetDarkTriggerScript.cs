using UnityEngine;
using System.Collections;

public class GetDarkTriggerScript : MonoBehaviour {
	
	bool startGettingDark = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if(startGettingDark == true)
		{
			RenderSettings.ambientLight = new Color(RenderSettings.ambientLight.r - 0.002f, RenderSettings.ambientLight.g - 0.002f, RenderSettings.ambientLight.b - 0.002f, 1.0f);
			Camera.main.backgroundColor = new Color(Camera.main.backgroundColor.r - 0.002f, Camera.main.backgroundColor.g - 0.002f, Camera.main.backgroundColor.b - 0.002f, 1.0f);		
			
			if(Camera.main.backgroundColor.b <= 0)
			{				
				startGettingDark = false;	
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.name == "Player")
			startGettingDark = true;
	}
}
