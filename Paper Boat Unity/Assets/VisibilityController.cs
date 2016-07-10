using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisibilityController : MonoBehaviour {

	public GameObject title;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (title.GetComponent<Text> ().text != "Problem with internet connection") {
			gameObject.SetActive (false);
		} else {
			gameObject.SetActive (true);
		}
	}
}
