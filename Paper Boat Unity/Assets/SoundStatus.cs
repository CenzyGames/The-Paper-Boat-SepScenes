using UnityEngine;
using System.Collections;

public class SoundStatus : MonoBehaviour {
	
	public AudioSource BoatCollision;
	public AudioSource Ripple;
	public AudioSource WaterFlow;
	public AudioSource SlipCollision;
	public AudioSource ButtonClick;
	public AudioSource BackgroundMusic;

	bool sound = true;
	// Use this for initialization
	void Awake () 
	{
		if (PlayerPrefs.GetString ("Sound") == "false") {
			sound = false;
		} else {
			sound = true;
		}
	}

	void Update()
	{
		BoatCollision.mute = sound;
		Ripple.mute = sound;
		WaterFlow.mute = sound;
		SlipCollision.mute = sound;
		ButtonClick.mute = sound;
		BackgroundMusic.mute = sound;
	}

	public void BoatCollisionSound()
	{
		BoatCollision.Play ();
	}

	public void RippleSound()
	{
		Ripple.Play();
	}

	public void WaterFlowSound()
	{
		WaterFlow.Play();
	}

	public void SlipCollisionSound()
	{
		if(SlipCollision != null)
		SlipCollision.Play();
	}

	public void SoundOn()
	{
		PlayerPrefs.SetString ("Sound" , "false");
		sound = false;
	}

	public void SoundOff()
	{
		PlayerPrefs.SetString ("Sound" , "true");
		sound = true;

	}

	void OnDisable()
	{
		SoundOn ();
		//PlayerPrefs.DeleteKey ("Instruct63");
		//Debug.Log ("LAST");
	}




}
