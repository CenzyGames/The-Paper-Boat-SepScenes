using UnityEngine;
using System.Collections;

public class destroyripple : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("--" + col.gameObject.tag);
		Debug.Log ("---" + gameObject.name);
		switch (col.gameObject.tag) 
		{
		case "boat":
			Destroy (gameObject);
			break;

		default :
			break;
			
		}
	}
}

