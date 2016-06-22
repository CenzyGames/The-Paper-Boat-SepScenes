using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
		Application.LoadLevel("GamePlay");
		Debug.Log("PLY GME");
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
}
