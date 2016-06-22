using UnityEngine;
using System.Collections;

public class lilyScript : MonoBehaviour{
    public GameObject ripple;
    GameObject ripple_clone;
    public float rippleDelay;

	float timer = 1.0f;
	int minutes =1;
	int seconds=1;
	
	bool increaseSpeed = true;

	void Start ()
    {
       transform.position = new Vector3(transform.position.x, 0.05f, Random.Range(0.6f, 1.6f) * -1);
        StartCoroutine("createRipple");
		StartCoroutine ("move");
    }

    IEnumerator createRipple()
    {
        ripple_clone = Instantiate(ripple, transform.position, Quaternion.identity) as GameObject;
        ripple_clone.GetComponent<rippleScript>().delay = 0.75f;
        ripple_clone.GetComponent<rippleScript>().rippleScale = 0.001f;
        ripple_clone.transform.parent = transform;
        yield return new WaitForSeconds(rippleDelay);
        StartCoroutine("createRipple");
    }

	IEnumerator move()
	{
	
		transform.Translate(-0.03f, 0, 0) ;				//rc -- oRIGINAL --transform.Translate(-0.01f, 0, 0) ;
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
				timer = 1.0f;
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
			Debug.Log ("Lillypad --- > Boat");
		}


	}
}
