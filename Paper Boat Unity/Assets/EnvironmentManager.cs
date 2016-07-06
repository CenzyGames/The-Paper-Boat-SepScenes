using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour {

	public  Texture defaultTexture;
	public  Texture snowTexture;
	public Texture WaterSnowTexture;
	public Texture WaterDefaultTexture;

	public GameObject[] gameObjects;
	public GameObject[] WaterSurfaces;
	// Use this for initialization
	void Start () 
	{
		gameObjects = GameObject.FindGameObjectsWithTag ("TextureSnow");
		WaterSurfaces = GameObject.FindGameObjectsWithTag ("WaterSurface");
		LoadEnviroment ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void LoadEnviroment()
	{
		switch (PlayerPrefs.GetInt ("environment")) 
		{
		case 0:
			changeTextureToDefault ();
			ChangeWaterToDefault ();
				break;
		case 1:
			changeTextureToSnow ();
			ChangeWaterToSnow ();	
			break;
		default:
			changeTextureToDefault ();
			ChangeWaterToDefault ();
				break;
		}
	}

	public void changeTextureToSnow()
	{
		PlayerPrefs.SetInt ("environment", 1);
		Debug.Log ("changing to snow");
		foreach (GameObject snow in gameObjects) 
		{
			if(snow.GetComponent<Renderer>().material.mainTexture != null)
			snow.GetComponent<Renderer>().material.mainTexture = snowTexture;
		}
		ChangeWaterToSnow ();

	}

	public void changeTextureToDefault()
	{
		PlayerPrefs.SetInt ("environment", 0);
		Debug.Log ("changing to Default");
		foreach (GameObject snow in gameObjects) 
		{
			if(snow.GetComponent<Renderer>().material.mainTexture != null)
				snow.GetComponent<Renderer>().material.mainTexture = defaultTexture;
		}
		ChangeWaterToDefault ();
	}

	public void ChangeWaterToSnow()
	{
		PlayerPrefs.SetInt ("environment", 1);
		foreach (GameObject WaterSurface in WaterSurfaces) 
		{
			if(WaterSurface.GetComponent<Renderer>().material.mainTexture != null)
				WaterSurface.GetComponent<Renderer>().material.mainTexture = WaterSnowTexture;
		}
	}

	public void ChangeWaterToDefault()
	{
		PlayerPrefs.SetInt ("environment", 0);
		foreach (GameObject WaterSurface in WaterSurfaces) 
		{
			if(WaterSurface.GetComponent<Renderer>().material.mainTexture != null)
				WaterSurface.GetComponent<Renderer>().material.mainTexture = WaterDefaultTexture;
		}
	}
}
