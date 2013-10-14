using UnityEngine;
using System.Collections;

public class CutsceneScript : MonoBehaviour 
{
	public Texture[] frames;
	int currentFrame = 0;
	
	public string nextScene = "Title";

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		renderer.material.mainTexture = frames[currentFrame];
		
		if(Input.GetMouseButtonDown(0))
		{
			++currentFrame;			
			if(currentFrame >= frames.Length)
				Application.LoadLevel(nextScene);
		}
	}
}
