using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	//public GameObject Image;
	int fadeTimeStart = 8;
	//public int fadeTimeEnd;
	// Use this for initialization
	void Start () 
	{
		if(Application.loadedLevelName == "SplashScreen")
		StartCoroutine("StartTimer");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void MenuSceneLoad()
	{
		Application.LoadLevel("Menu");
	}

	public void GamePlaySceneLoad()
	{
		if (PlayerPrefs.GetInt ("TapCount") >= 5) {
			Application.LoadLevel ("GamePlay");
		} else 
		{
			Application.LoadLevel ("Instructions");
		}
	}

	public void GameOverSceneLoad()
	{
		Application.LoadLevel("GameOver");
	}

	public void BoatMenuSceneLoad()
	{
		Application.LoadLevel("BoatMenu");
	}

	public void ExitSceneLoad()
	{
		Application.LoadLevel("Exit Menu");
	}

	public void EndGame()
	{
		Application.Quit();
		Debug.Log("Exit Quit");
	}	

	IEnumerator StartTimer()
	{
		
		yield return new WaitForSeconds(fadeTimeStart);
		Application.LoadLevel ("Menu");
		
	}	
}
