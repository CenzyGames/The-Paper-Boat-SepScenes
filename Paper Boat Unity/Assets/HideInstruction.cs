using UnityEngine;
using System.Collections;

public class HideInstruction : MonoBehaviour {

	public GameObject canvas;
	public GameObject Palm1;
	public GameObject Palm2;
	//public GameObject ripple;
	//GameObject rippleClone;

	int timeToHide = 6;
	Ray ray;
	RaycastHit hit;



	void Start () 
	{
		
//	//PlayerPrefs.DeleteKey ("Instruct63");
//		if (!PlayerPrefs.HasKey ("Instruct63")) {
//			PlayerPrefs.SetInt ("Instruct63", 1);
//			Debug.Log ("Set to 1");
//		} 
//
//		if (PlayerPrefs.GetInt ("Instruct63") == 1) 
//		{
//			Palm2.gameObject.SetActive (true);
//			//Palm1.gameObject.SetActive (true);
//		}
//		else if (PlayerPrefs.GetInt ("Instruct63") == 0 )
//		{
//			Palm1.gameObject.SetActive (false);
//			Palm2.gameObject.SetActive (false);
//		}
	}

	void Update()
	{
//		Debug.Log (	PlayerPrefs.GetInt ("Instruct63"));

		if(canvas.transform.FindChild("GameMenus").gameObject.transform.FindChild("Revive_Menu").gameObject.activeInHierarchy == true)
		{
			gameObject.SetActive (false);
		}

		if(canvas.transform.FindChild("GameMenus").gameObject.transform.FindChild("Pause_Menu").gameObject.activeInHierarchy == true)
		{
			gameObject.SetActive (false);
		}

//		if (Time.timeScale != 0) 
//		{		// RC ---- To form ripple when the game is not paused.
//			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			Debug.DrawRay (ray.origin, ray.direction * 10, Color.red);
//			if (Input.GetMouseButtonDown (0))
//			{
//				if (Physics.Raycast (ray, out hit, 50))
//				{	
//					if (PlayerPrefs.GetInt("Instruct63") == 1)
//					{
//						if (hit.collider.name == "Palm2") {
//							Palm2.gameObject.SetActive (false);
//							Palm1.gameObject.SetActive (true);
//						//	rippleClone =Instantiate(ripple, hit.point, Quaternion.identity) as GameObject;
//							GameObject.Find ("InputManager").GetComponent<inputManagerScript> ().RippleForm ();
//						}
//						if (hit.collider.name == "Palm1") {
//							Palm1.gameObject.SetActive (false);
//							GameObject.Find ("enemyManager").GetComponent<enemyManagerScript>().StartSpawn();
//							PlayerPrefs.SetInt ("Instruct63", 0);
//							GameObject.Find ("InputManager").GetComponent<inputManagerScript> ().RippleForm ();
//						//	rippleClone = Instantiate(ripple, hit.point, Quaternion.identity) as GameObject;
//						}
//					}
//				}
//			}
//		}
	}
		


}
