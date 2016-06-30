using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {
	GameObject currentScore;
	GameObject highScore;

	// Use this for initialization
	void Start () 
	{
		currentScore = GameObject.Find ("Value");
		highScore = GameObject.Find ("HighScoreDisplay");
		currentScore.GetComponent<Text> ().text = "" + PlayerPrefs.GetString ("CurrentScore");
		highScore.GetComponent<Text>().text = PlayerPrefs.GetFloat("HighScore") +  "" ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
		

}
