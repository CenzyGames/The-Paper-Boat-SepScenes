using UnityEngine;
using System.Collections;

public class CataractRipple : MonoBehaviour {

	public GameObject ripple;
	GameObject ripple_clone;
	public float rippleDelay;

	float timer = 1.0f;
	int minutes =1;
	int seconds=1;

	bool increaseSpeed = true;

	void Start ()
	{
		transform.position = new Vector3(transform.position.x, 0.05f, transform.position.y* -1);
		StartCoroutine("createRipple");
	}

	IEnumerator createRipple()
	{
		ripple_clone = Instantiate(ripple, transform.position, Quaternion.identity) as GameObject;
		ripple_clone.GetComponent<rippleScript>().delay = 0.75f;
		ripple_clone.GetComponent<rippleScript>().rippleScale = 0.001f;
		ripple_clone.transform.parent = transform;
		Destroy(ripple_clone ,1.0f);
		yield return new WaitForSeconds(rippleDelay);

		StartCoroutine("createRipple");
	}

	void OnTriggerEnter(Collider pCol)
	{
		if (pCol.gameObject.tag == "boat") 
		{
			Debug.Log ("Collided with boat");
			Vector3 myForce = new Vector3(-45.0f, 150.0f, 0.0f);
			pCol.gameObject.GetComponent<Rigidbody> ().AddForce (myForce);
			GameObject.FindGameObjectWithTag ("script").GetComponent<AudioSource> ().Play ();
		}
	}
}
