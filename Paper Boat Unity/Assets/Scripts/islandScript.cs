using UnityEngine;
using System.Collections;

public class islandScript : MonoBehaviour
{

	float timer = 3.0f;
	int minutes =1;
	int seconds=3;
	bool increaseSpeed = true;
	void Start ()
    {
        transform.position = new Vector3(transform.position.x, 0.05f, Random.Range(0.3f, 1.5f) * -1);
		// transform.position = new Vector3(transform.position.x, 0.05f, Random.Range(0.8f, 1.2f) * -1);
		StartCoroutine ("move");
    }

	IEnumerator move()
	{
	//	TimeIncrease();
	
		transform.Translate(-0.03f, 0, 0) ;				//Original - transform.Translate(-0.01f, 0, 0) ;
		if (transform.position.x < -4)
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
				timer = 3.0f;
			}
			timer = timer - Time.deltaTime;
		}
		Debug.Log(""+timer);
	}

	void OnTriggerEnter(Collider pCol)
	{
		if (pCol.gameObject.tag == "boat")
		{
			GameObject.FindGameObjectWithTag ("script").GetComponent<AudioSource> ().Play ();
			Debug.Log ("Island --- > Boat");
		}


	}

}
