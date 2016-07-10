using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoInternetFade : MonoBehaviour {

	float alphaValue = 1;
	// Use this for initialization
	void Start () {
	
		//gameObject.SetActive (false);		// Hides the gameObject on load

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameObject.activeInHierarchy == true) 
		{
			
			if (alphaValue > 0.2f)
			{
				StartCoroutine("StartFade");
			}

			gameObject.GetComponent<Text> ().color = new Color(1, 1, 1, alphaValue);

			//Debug.Log ("Decreasing value" + Time.deltaTime);
		}

		if(gameObject.GetComponent<Text> ().color.a < 0.2f)
		{
			alphaValue = 1.0f;
			gameObject.GetComponent<Text> ().color = new Color(1, 1, 1, 1);
			gameObject.SetActive (false); // Hides the gameObject after it is faded

		}
	}

	IEnumerator StartFade()
	{
		alphaValue -= alphaValue/60;
		yield return new WaitForSeconds(0.01f);
		StartCoroutine("StartFade");
	}
}
