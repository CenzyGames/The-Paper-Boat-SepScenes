using UnityEngine;
using System.Collections;

public class ObjectsTrack : MonoBehaviour {

	GameObject[] fish;
	GameObject[] duck;
	GameObject[] island;
	GameObject[] lillypad;

	// Use this for initialization
	void Start () 
	{
		//Starts the functions to store all enemies available in the scene
	//	StoreFish();
	//	StoreDuck ();
	//	StoreIsland ();
	//	Storelillypad ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// checks the value of the "Show instructions" and if the insctructions are shown it will hide all the enemies available in the gameplay
//		if (PlayerPrefs.GetInt("InstructionOn") == 0)
//		{
//			HideFishes ();
//			HideDucks ();
//			HideIsland ();
//			HideLillypad ();
//		}
	}

	// To store all fish Gameobjects avaialble in the gameplay
	void StoreFish()
	{
		fish = GameObject.FindGameObjectsWithTag ("fish");

	}

	// To store all Duck Gameobjects avaialble in the gameplay
	void StoreDuck()
	{
		duck = GameObject.FindGameObjectsWithTag ("duck");
	}

	// To store all islands Gameobjects avaialble in the gameplay
	void StoreIsland()
	{
		island = GameObject.FindGameObjectsWithTag ("island");
	}

	// To store all Liypad Gameobjects avaialble in the gameplay
	void Storelillypad()
	{
		lillypad = GameObject.FindGameObjectsWithTag ("lillypad");
	}

	void HideFishes()
	{
		if (fish.Length != 0) 
		{
			foreach (GameObject gameObject in fish) 
			{
				gameObject.SetActive (false);
			}
		}
	}

	void HideDucks()
	{
		if (duck.Length != 0) 
		{
			foreach (GameObject gameObject in duck) 
			{
				gameObject.SetActive (false);
			}
		}
	}

	void HideIsland()
	{
		if (island.Length != 0) 
		{
			foreach (GameObject gameObject in island) 
			{
				gameObject.SetActive (false);
			}
		}
	}

	void HideLillypad()
	{
		if (lillypad.Length != 0) 
		{
			foreach (GameObject gameObject in lillypad) 
			{
				gameObject.SetActive (false);
			}
		}
	}

	void ShowEnemies()
	{
		if (fish.Length != 0) 
		{
			foreach (GameObject gameObject in fish) 
			{
				gameObject.SetActive (true);
			}
		}
		if (duck.Length != 0) 
		{
			foreach (GameObject gameObject in duck) 
			{
				gameObject.SetActive (true);
			}
		}
		if (island.Length != 0) 
		{
			foreach (GameObject gameObject in island) 
			{
				gameObject.SetActive (true);
			}
		}
		if (lillypad.Length != 0) 
		{
			foreach (GameObject gameObject in lillypad) 
			{
				gameObject.SetActive (true);
			}
		}
	}
}
