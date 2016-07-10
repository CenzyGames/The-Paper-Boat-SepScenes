using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour {

	float alphaValue = 0.1f;
	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () 
	{
		Debug.Log (alphaValue);
		if (gameObject.activeInHierarchy == true) 
		{

			if (alphaValue >= 0.1f)
			{
				StartCoroutine("StartFade");
			}

			gameObject.GetComponent<Text> ().color = new Color(1, 1, 1, alphaValue);

		//	Debug.Log ("Decreasing value" + Time.deltaTime);
		}

		if(gameObject.GetComponent<Text> ().color.a > 0.96f)
		{

			gameObject.GetComponent<Text> ().color = new Color(1, 1, 1, 1);
			//gameObject.SetActive (false); // Hides the gameObject after it is faded

		}
	}

	IEnumerator StartFade()
	{
		alphaValue += alphaValue/6000;
		if (gameObject.GetComponent<Text> ().color.a > 0.9f) {
			yield return new WaitForSeconds (0);
			//alphaValue = 0.1f;
		} else {
			yield return new WaitForSeconds (0);
		}
			StartCoroutine("StartFade");
	}
		
}
