using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TapCounterForInstructions : MonoBehaviour {

	public GameObject PlayButton;
	public GameObject Instrcuction1;
	public GameObject Instrcuction2;
	Ray ray;
	RaycastHit hit;

	static int count = 0;
	// Use this for initialization
	void Start () 
	{
		PlayButton.SetActive (false);
		if (PlayerPrefs.GetInt ("TapCount") == 5) 
		{
			Debug.Log ("Load Game play");
			//Application.LoadLevel ("GamePlay");
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.timeScale!=0)		// RC ---- To form ripple when the game is not paused.
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction*10, Color.red);
			if (Input.GetMouseButtonDown(0))
			{
				if (Physics.Raycast(ray, out hit, 10))				// Original - if (Physics.Raycast(ray, out hit, 50))
				{
					if (hit.collider.name == "collider" || hit.collider.name == "Plane")
					{
						TapCounter ();
					}
				}
			}
		}
	
	}

	void TapCounter()
	{
		if (count/2 > 1) 
		{
			ShowNextInstructions ();
		}
		if (count >= 5) 
		{
			PlayerPrefs.SetInt ("TapCount", 5);
			ShowPlayButton ();
		}
		if(count/2<5)
			count++;
	}

	void ShowPlayButton()
	{
		PlayButton.SetActive (true);
	}

	void ShowNextInstructions ()
	{
	//	if (Instrcuction1.GetComponent<Text> ().color.a < 0.2f) 
		{
			Instrcuction1.SetActive (false);
			Instrcuction2.SetActive (true);
		}
	}




}
