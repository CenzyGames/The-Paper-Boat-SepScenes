using UnityEngine;
using System.Collections;

public class BoatCollision : MonoBehaviour {
	int collisionCount =0;
	// Use this for initialization
	void Start () 
	{
		collisionCount =0;
		PlayerPrefs.SetInt("collisionCount", collisionCount);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnCollisionEnter(Collision col)
	{
		collisionCount = PlayerPrefs.GetInt("collisionCount");
		switch(col.gameObject.tag)
		{
			case "duck" : collisionCount+=1; PlayerPrefs.SetInt("collisionCount", collisionCount);  break;
			case "fish" : collisionCount+=1; PlayerPrefs.SetInt("collisionCount", collisionCount); break;
			case "lillypad" : collisionCount+=1; PlayerPrefs.SetInt("collisionCount", collisionCount); break;
			case "island" : collisionCount+=1; PlayerPrefs.SetInt("collisionCount", collisionCount); break;
			default : break;
		}

	}

	void BoatSink()
	{
		collisionCount = PlayerPrefs.GetInt("collisionCount");
	}
	
}
