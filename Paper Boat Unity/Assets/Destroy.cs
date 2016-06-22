using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {


	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject,1);

		if (gameObject.name == "Particle System(Clone)") 
		{
			transform.Rotate (-90,0,0);			//To rotate particle system
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
