using UnityEngine;
using System.Collections;

public class StoneCollisions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider pCol)
	{
		if (pCol.gameObject.tag == "boat")
		{
			GameObject.FindGameObjectWithTag ("script").GetComponent<AudioSource> ().Play ();
			Debug.Log ("Stone --- > Boat");
		}


	}
}
