using UnityEngine;
using System.Collections;

public class ColorSequence : MonoBehaviour {
	[System.Serializable]
	public class ColorOptions
	{
		[SerializeField] internal Color color1 = Color.red;
		[SerializeField] internal Color color2 = Color.blue;
		[SerializeField] internal Color color3 = Color.yellow;
		[SerializeField] internal Color color4 = Color.white;
		//[SerializeField] internal Color color5 = Color.red;
	}
	public enum ColorSelector {Color1,Color2,Color3,Color4}

	public float LerpTime = 20.0f;
	public float fadeTime = 10.0f;
	public ColorOptions colorOptions;
	
	private Color startColor;
	
	float timer = 1.0f;
	int minutes =1;
	int seconds=1;

	int count = 1;
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine("fade");
	}
	
	// Update is called once per frame
	void Update () 
	{
		minutes =(int) timer / 60;
		seconds =(int) timer % 60;
		timer = timer + Time.deltaTime;
		//DirectionLightFade();
	}
		


	IEnumerator fade()
	{
		yield return new WaitForSeconds(0.1f);
		fadeTime += 0.005f;
		switch (count) 
		{
		case 1:
			GetComponent<Light> ().color = Color.Lerp (colorOptions.color1, colorOptions.color2, fadeTime);
			break;
		case 2: GetComponent<Light> ().color = Color.Lerp (colorOptions.color2, colorOptions.color3, fadeTime);
			break;
		case 3: GetComponent<Light> ().color = Color.Lerp (colorOptions.color3, colorOptions.color4, fadeTime);
			break;
		case 4: GetComponent<Light> ().color = Color.Lerp (colorOptions.color4, colorOptions.color1, fadeTime);
			break;
		default: break;
		}
			
		if (fadeTime >= 1)
		{
			fadeTime = 0.01f;
			count++;
			if (count > 4) 
			{
				count = 1;
			}
		}
		StartCoroutine("fade");
	}
	
}
