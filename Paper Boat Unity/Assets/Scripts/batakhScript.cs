using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class batakhScript : MonoBehaviour {
   	float length;		
	float speed;		
	int goUp;		

    public GameObject ripple;
    GameObject ripple_clone;
    public float rippleDelay;

    void Start ()
    {
        goUp = Random.Range(0, 2);
		if(goUp==0)
		{
			transform.position = new Vector3(transform.position.x -3.0f, transform.position.y, -1.3f);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
			length = 1;
		}
		else
		{
			transform.position = new Vector3(transform.position.x -3.0f, transform.position.y, -0.5f);
			transform.eulerAngles  = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z); 
			length = -1;
		}
		speed = Random.Range(0.002f, 0.004f);

		Debug.Log (goUp + "-" + speed);
        StartCoroutine("createRipple"); 
		StartCoroutine ("move");
    }

	IEnumerator move()
	{
		if(length==1)
		{
			transform.Translate(-0.02f, 0, speed*length*(1.5f)) ;				//RC - original -- 	transform.Translate(-0.01f, 0, speed*length) ;	
		}
		else
		{
			transform.Translate(0.02f, 0, (-1)*speed*length*(1.5f)) ;	
		}
		if (transform.position.x < -1.0f)		//--- RC
		{
			Destroy(gameObject,0.1f);
		}
		yield return new WaitForSeconds (0.01f);
		StartCoroutine ("move");
	}

	IEnumerator createRipple()
	{
		ripple_clone = Instantiate(ripple, transform.position + (Vector3.up * 0.035f), Quaternion.identity) as GameObject;
		ripple_clone.GetComponent<rippleScript>().delay = 1.0f;
		ripple_clone.GetComponent<rippleScript>().rippleScale = 0.001f;
		ripple_clone.GetComponent<rippleScript>().move = Vector3.left * 0.01f;
		yield return new WaitForSeconds(rippleDelay);
		StartCoroutine("createRipple");
	}

	void OnTriggerEnter(Collider pCol)
	{
		if (pCol.gameObject.tag == "boat")
		{
			GameObject.FindGameObjectWithTag ("script").GetComponent<AudioSource> ().Play ();
			Debug.Log ("Batakh --- > Boat");
		}


	}

}
