using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

	//public GameObject Image;
	public int fadeTimeStart;
	//public int fadeTimeEnd;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine("StartTimer");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	IEnumerator StartTimer()
	{

		yield return new WaitForSeconds(fadeTimeStart);
		Application.LoadLevel ("GamePlay");

	}	
		
}
