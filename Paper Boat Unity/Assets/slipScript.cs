using UnityEngine;
using System.Collections;

public class slipScript : MonoBehaviour {

	float timer = 3.0f;
	int minutes =1;
	int seconds=3;
	bool increaseSpeed = true;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, 0.05f, Random.Range(0.6f, 1.6f) * -1);
		transform.eulerAngles = new Vector3(90,0,0);
		StartCoroutine ("move");
    }

	IEnumerator move()
	{
		transform.Translate(-0.02f* Time.timeScale, 0, 0);		//RC -- original --transform.Translate(-0.01f* Time.timeScale, 0, 0) ;
		if (transform.position.x < -6)
		{
			Destroy(gameObject,0.1f);
		}
		yield return new WaitForSeconds (0.01f);
		StartCoroutine ("move");
	}

	void TimeIncrease()
	{
		minutes =(int) timer / 60;
		seconds =(int) timer % 60;
		//	seconds = seconds/10;
		Debug.Log(seconds);
		if (seconds >= 1 && seconds <= 5 && increaseSpeed == true) 
		{
			if(seconds >= 5)
			increaseSpeed = false;
			timer = timer + Time.deltaTime/10;
		}
		else if(seconds <= 5 && seconds >= 10 && increaseSpeed == false)
		{		
			if(seconds >= 10)
			{
				increaseSpeed = true;
				timer = 1.0f;
			}
			timer = timer - Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider pCol)
	{
		if (pCol.gameObject.tag == "boat")
		{
			GameObject.FindGameObjectWithTag ("slipsound").GetComponent<AudioSource> ().Play ();
		}

	}
}
