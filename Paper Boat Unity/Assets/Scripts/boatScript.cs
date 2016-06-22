using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class boatScript : MonoBehaviour
{
	public Vector3 vel;
    float newVelX;
    float newVelZ;

    float xPos;
    float moveX;

    Vector3 cameraPos;
    float difference;
    float camXPos;
    float xDiff;

    public GameObject canvas;

	int reviveCount;

	static bool isCollided = false; // RC --- to check whether boat has been collided or not
	bool gameRunning = true;

	public GameObject particleSystem;

	bool boatsink = false;

	public SoundStatus sound;
	GameObject reviveMenu;

    void Start()
    {
		isCollided = false; /// RC --- to set to false ion start
		PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to false ion start
		reviveCount = 1;
		canvas = GameObject.Find("Canvas");
        xPos = transform.position.x;
        cameraPos = Camera.main.transform.position;
        difference = cameraPos.z - transform.position.z;
        camXPos = transform.position.z + difference;
		StartCoroutine ("scoreAdd");
		PlayerPrefs.SetString("gameRunning", "true");		//RC --- !!
		boatsink=false;

		reviveMenu = canvas.transform.FindChild ("GameMenus").transform.FindChild ("Revive_Menu").gameObject;
    }

	/*increase score with time*/
	IEnumerator scoreAdd()
	{
		yield return new WaitForSeconds (1);
		if (PlayerPrefs.GetInt ("Instruct63") == 0) {
			canvas.GetComponent<uiScript> ().score++;
		}
			
		StartCoroutine ("scoreAdd");
	}

    void Update ()
    {
		if(PlayerPrefs.GetString("gameRunning")== "true")			// RC ---!! to activate use slips option
		{
			/*resets the camera angle to 0 after its rotated*/
			float angle = Mathf.LerpAngle (transform.eulerAngles.y, 180, Time.deltaTime);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, angle , transform.eulerAngles.z);
			
			/*resets boats's X position*/
       		 if (xPos - transform.position.x >= 0)
       	 	{
        		    moveX = Mathf.Lerp(transform.position.x, xPos, Time.deltaTime * (xPos - transform.position.x));
       		 }
      		  else
      	 	 {
       	 	    moveX = Mathf.Lerp(transform.position.x, xPos, Time.deltaTime);
       		 }
       	 	transform.position = new Vector3(moveX, transform.position.y, transform.position.z);
       		 camXPos = Mathf.Lerp(Camera.main.transform.position.z, transform.position.z + difference, Time.deltaTime*2);
       		 Camera.main.transform.position = new Vector3(cameraPos.x,cameraPos.y,camXPos);

			/*resets boat's velocity*/
			vel = GetComponent<Rigidbody> ().velocity;
      	  	newVelX = Mathf.Lerp(vel.x, 0, Time.deltaTime);
       	 	newVelZ = Mathf.Lerp (vel.z, 0, Time.deltaTime);
			GetComponent<Rigidbody> ().velocity = new Vector3(newVelX,0,newVelZ);
			/*game over condition*/
			if (transform.position.x < 1.8f)
     	   {
				Debug.Log("Collisions - " + PlayerPrefs.GetInt("collisionCount"));

					canvas.transform.FindChild("GameMenus").transform.FindChild("Revive_Menu").gameObject.SetActive(true);				// RC ---- canvas.transform.FindChild("GameMenus").gameObject.SetActive(true);
					canvas.transform.FindChild("GameMenus").transform.FindChild("Revive_Menu").transform.FindChild("Slips").gameObject.SetActive(true);

					if(PlayerPrefs.GetString("ReviveByAds") == "False")
					{
						canvas.transform.FindChild("GameMenus").transform.FindChild("Revive_Menu").FindChild("showAd").gameObject.SetActive(false);
						canvas.transform.FindChild("GameMenus").transform.FindChild("Revive_Menu").FindChild("NoInternet").gameObject.SetActive(false);
					}
					canvas.GetComponent<uiScript>().checkSlips(reviveCount*100);//slips deducted
					reviveCount *= 2;
					PlayerPrefs.SetString("gameRunning","false");		//RC --- !!
					Time.timeScale = 0;
				if (reviveMenu != null && PlayerPrefs.GetString ("ReviveByAds") == "False") 
				{
					reviveMenu.transform.FindChild ("Slips").gameObject.transform.FindChild ("Text").GetComponent<Text> ().text = " " + (reviveCount / 4) * 100 + "";
					reviveMenu.transform.FindChild ("No Slips").gameObject.transform.FindChild ("Text").GetComponent<Text> ().text = " " + (reviveCount / 4) * 100 + "";
				} 
				else 
				{
					reviveMenu.transform.FindChild ("Slips").gameObject.transform.FindChild ("Text").GetComponent<Text> ().text = " " + (reviveCount / 2) * 100 + "";
					reviveMenu.transform.FindChild ("No Slips").gameObject.transform.FindChild ("Text").GetComponent<Text> ().text = " " + (reviveCount / 2) * 100 + "";
				}
      	  }
		
		}
	}
		

    void OnTriggerEnter(Collider pCol)
    {
        if (pCol.gameObject.tag == "slip")
        {
			pCol.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			pCol.gameObject.GetComponent<BoxCollider>().enabled = false;
			pCol.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			Destroy(pCol.gameObject);
            canvas.GetComponent<uiScript>().addSlips();
			//Play animation of slip------------------------------RC
			Instantiate( particleSystem, gameObject.transform.position,Quaternion.identity);
        }

		if(pCol.gameObject.tag == "duck")
		{
			isCollided = true;
			PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to true 
		}
		if(pCol.gameObject.tag == "fish")
		{
			isCollided = true;
			PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to true 
		}
		if(pCol.gameObject.tag == "lillypad")
		{
			isCollided = true;
			PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to true 
		}
		if(pCol.gameObject.tag == "island")
		{
			isCollided = true;
			PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to true 
		}
		if(pCol.gameObject.tag == "left")
		{
			Debug.Log("Collided with Left wall");
		}

    }
		
}
