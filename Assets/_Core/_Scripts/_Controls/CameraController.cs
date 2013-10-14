using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(-5 * Time.deltaTime, 0, 0);
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(5 * Time.deltaTime, 0, 0);
		}
	}
}
