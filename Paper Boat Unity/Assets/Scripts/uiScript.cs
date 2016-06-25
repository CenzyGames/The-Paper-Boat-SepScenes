using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms;// RC

public class uiScript : MonoBehaviour {
    public GameObject[] boats;
    public int[] boatCost;
    public bool[] boatBought;
	int[] boatMultiplier;

    public Vector3 moveTo;
    public Vector3 moveBy;

    public GameObject manager;
    public GameObject bridge;
    public GameObject ui;
    public GameObject gameMenu;

	public GameObject slipButton;

    bool move;

    public int slips;
    public Text fpsText;
	public Text slipText;

    int boatSpawnIndex;

	public int score;
	
	public GameObject PauseMenu;		// RC --- Required to show pause menu screen
	public GameObject InternetMessage;		// RC --- Required to show pause menu screen

	public Text distanceXMultiplier;
	public GameObject SubmitScoreButton;

	public GameObject Instruction;

	string message ="";	// RC --- Required to show message on the screen
	string internetNotconnected = "Internet not connected";
	string blank = "";
	string userNotLoggedIn = "User not Logged In";
	string loggedIn = "Player Logged In";

	IAchievement achievement ;

	int count =1;// RC --- to deduct slips in power of 2. Used in the method useslips()

	int slipsCollectedInOneGame =0;



	void Awake()
	{
		PlayGamesPlatform.Activate();

	}

    void Start()
    {
		/*boat multiplier*/
		boatMultiplier = new int[7];
		boatMultiplier[0] = 1;
		boatMultiplier[1] = 2;
		boatMultiplier[2] = 3;
		boatMultiplier[3] = 4;
		boatMultiplier[4] = 5;
		boatMultiplier[5] = 6;
		boatMultiplier[6] = 7;
        
		/*set boats cost*/
        boatCost = new int[7];
        boatCost[0] = 1;
        boatCost[1] = 1;
        boatCost[2] = 1;
        boatCost[3] = 1;
        boatCost[4] = 1;
        boatCost[5] = 1;
		boatCost[6] = 1;

		boatBought = new bool[7]; //checks if a boat is bought
       	
		PlayerPrefs.SetString("ReviveByAds","True");	//--- RC to check that revive button is pressed only once
        
	//PlayerPrefs.SetInt("slips", 99999);
        
		/*get slips value from registry*/
		slips = PlayerPrefs.GetInt("slips", 0);
		slipText.text = " " + slips.ToString ();
        
		/*sets up camera for ui*/
		move = true;
        moveTo = Camera.main.transform.position;

		message = blank;		// RC ----- to hide Internetcheck message

		PlayerPrefs.SetString("BoatRevived", "false");
		slipsCollectedInOneGame =0;
		PlayerPrefs.SetInt("slipsCollectedInOneGame", slipsCollectedInOneGame);

		boatSpawnIndex = PlayerPrefs.GetInt ("BoatNumber");
		playGame(0);
//		Debug.Log("GME STRTED aGIN");
	//	if (GameObject.Find ("Main_Menu").gameObject.activeInHierarchy == true) {
//			Instruction.SetActive (false);
		//	Debug.Log ("Hide ---- 1");
	//	} 
    }

    public void showAd()
    {
		Debug.Log("-----"+ PlayerPrefs.GetString("ReviveByAds"));
		if(PlayerPrefs.GetString("ReviveByAds") == "True")		//--- RC to check that revive button is pressed only once
		{
			if (Advertisement.IsReady("video"))				//if (Advertisement.IsReady())
        	{
				Debug.Log("-----1");
				Advertisement.Show("video", new ShowOptions {			//RC---
					resultCallback = result => {						//RC---
						Debug.Log(result.ToString());					//RC---
					}													//RC---
				});														//RC---  Previous --> //Advertisement.Show();
				PlayerPrefs.SetString("ReviveByAds","False");				//--- RC to check that revive button is pressed only once
				GameObject.FindGameObjectWithTag("boat").transform.position = new Vector3(2.7f,0.05f,-0.91f);	//RC --- To set the boat to previous locationa nd to resume the game
				gameMenu.transform.FindChild("Revive_Menu").gameObject.SetActive(false);			// RC --- transform.FindChild("GameMenus").gameObject.SetActive(false);
				PlayerPrefs.SetString("gameRunning","true");			//RC --- !!
				PlayerPrefs.SetInt("collisionCount",0); // RC - !!
				PlayerPrefs.SetString("BoatRevived", "true");
				Play();
			}		
        }
    }

	/*checks if the player has enough slips to revive*/
	public void checkSlips(int value)
	{
		if (slips - value < 0)
		{
			slipButton.SetActive(false);
		}
		
	}
	
	/*use Slips to revive*/
	public void useSlips()
	{
		Time.timeScale = 1;
		slips -= 100*count;
		count*=2;
		PlayerPrefs.SetInt("slips", slips);
		slipText.text = " " + slips.ToString ();
		gameMenu.transform.FindChild("Revive_Menu").gameObject.SetActive(false);					// Now working - RC --- transform.FindChild("GameMenus").gameObject.SetActive(false);//not working 
		GameObject.FindGameObjectWithTag("boat").transform.position = new Vector3(2.7f,0.05f,-0.91f);

		PlayerPrefs.SetString("gameRunning","true");			//RC --- !!
		PlayerPrefs.SetInt("collisionCount",0); // RC - !!
		PlayerPrefs.SetString("BoatRevived", "true");
		Play();
	}
	

    void Update()
    {
		BackHardwareKeyPressed ();
		if(Application.loadedLevelName == "GamePlay")
		{
			fpsText.text = score.ToString ()+ " m" ;//sets score text
			//CalculateDistance();
		}
		if (move)
        {
            moveBy = Vector3.MoveTowards(Camera.main.transform.position, moveTo, 1);
            Camera.main.transform.position = moveBy;
        }
							// RC ----- for functionallity of backhardwarekey
    }

	/*starts game*/
	public void playGame(int boatSpawnIndex)
    {
		Time.timeScale =1; 
		transform.FindChild("Score").gameObject.SetActive(true);
		//manager.SetActive(true);
		bridge.SetActive(false);
//		ui.SetActive(false);
        move = false;
		/*Instantiates the selected boat*/
		PlayerPrefs.SetInt("BoatNumber", boatSpawnIndex);		//RC --- To save the boat number and then to calculate the score.

		//LOAD THE BOAT SPAWNINDEX FROM PLAYERPREFS TO INITIALIZE THE BOAT BUY
        
		Instantiate(boats[boatSpawnIndex], new Vector3(2.7f, 0.05f, -0.91f), Quaternion.identity);

    }
	public void BackHardwareKeyPressed()
	{
		if(Input.GetKeyDown(KeyCode.Escape) )
		{
			PauseAndPlay();
		}
	}
	
	public void PauseAndPlay() //-- Hide pause menu
	{
		if(Time.timeScale==0)
		{
			Play();
		}
		else if(Time.timeScale==1)
		{
			PauseGame();
		}
	}


	public void submitScoreLeaderboard()
	{	
		Social.localUser.Authenticate (success => {
			if (success)
			{
				Social.ReportScore(score*100, "CgkIgr6bkeMCEAIQDg", (succes) => {							// handle success or failure
				});
				SubmitScoreButton.transform.FindChild("title").GetComponent<Text>().text = "Score subimitted";
			}
			else
			{
				SubmitScoreButton.transform.FindChild("title").GetComponent<Text>().text = "Problem with internet connection";
			}
		});
	}



    public void gotoPanel(float xVal)
    {
        moveTo = new Vector3(xVal, moveTo.y, moveTo.z);/*sets the destinantion of camera in UI*/
    }

	public void gotoPanelBoatMenu(int xVal)
	{
		switch(xVal)
		{
			case 0: if(CheckSlipsValue(0)){	//gotoPanel(17.93f); 
				ShowConfirmationMenu();}break;
			case 1: if(CheckSlipsValue(1000)){	//gotoPanel(17.93f); 
				ShowConfirmationMenu();}break;
			case 2: if(CheckSlipsValue(2000)){	//gotoPanel(17.93f);
				ShowConfirmationMenu();}break;
			case 3: if(CheckSlipsValue(3000)){	//gotoPanel(17.93f);
				ShowConfirmationMenu();}break;
			case 4: if(CheckSlipsValue(4500)){	//gotoPanel(17.93f);
				ShowConfirmationMenu();}break;
			case 5: if(CheckSlipsValue(6000)){	//gotoPanel(17.93f);
				ShowConfirmationMenu();}break;
			case 6: if(CheckSlipsValue(10000)){	//gotoPanel(17.93f);
				ShowConfirmationMenu();}break;
			default : break;
		} 
			
	}


	bool CheckSlipsValue(int value)
	{
		slips = PlayerPrefs.GetInt("slips");
		if (slips - value <= 0)
		{
			print("kangaal Manushya");
			ui.gameObject.transform.FindChild("Boat_Menu").gameObject.transform.FindChild("Not Enough Slips").gameObject.SetActive(true);
			return false;
		}
		else
		{
			return true;
		} 
	}

	
	public void buyBoat(int index)
	{
		BoatBuyConfirmation(index);
		if (boatBought[index])
		{
			boatSpawnIndex = index;
		}
		else
		{
			boatSpawnIndex = index;
		}
	}

	void BoatBuyConfirmation(int index)
	{
		slips = PlayerPrefs.GetInt("slips");
		switch(index)
		{
		case 0: PlayerPrefs.SetInt("slips", slips ); Debug.Log("00000"); PlayerPrefs.SetInt("DefaultBoat", 0); break;
		case 1: PlayerPrefs.SetInt("slips", slips - 1000);Debug.Log("00001");PlayerPrefs.SetInt("DefaultBoat", 1); break;
		case 2: PlayerPrefs.SetInt("slips", slips - 2000);Debug.Log("00002");PlayerPrefs.SetInt("DefaultBoat", 2); break;
		case 3: PlayerPrefs.SetInt("slips", slips - 3000);Debug.Log("00003");PlayerPrefs.SetInt("DefaultBoat", 3); break;
		case 4: PlayerPrefs.SetInt("slips", slips - 4500);Debug.Log("00004");PlayerPrefs.SetInt("DefaultBoat", 4); break;
		case 5: PlayerPrefs.SetInt("slips", slips - 6000);Debug.Log("00005");PlayerPrefs.SetInt("DefaultBoat", 5); break;
		case 6: PlayerPrefs.SetInt("slips", slips - 10000);Debug.Log("00006");PlayerPrefs.SetInt("DefaultBoat", 6); break;
		default : break;
		}
			slips = PlayerPrefs.GetInt("slips");
			PlayerPrefs.Save();
			slipText.text = " " + slips.ToString ();
			print("Paise wala");
			HideNotEnoughSlips();
	}

	public void HideNotEnoughSlips()
	{
		ui.gameObject.transform.FindChild("Boat_Menu").gameObject.transform.FindChild("Not Enough Slips").gameObject.SetActive(false);
	}

	/*buy boats and themes*/
    public void buyItem(int value)
    {
        if (slips - value < 0)
        {
            print("kangaal Manushlogoutya");
        }
        else
        {
            slips -= value;
            PlayerPrefs.SetInt("slips", slips);
            print("Bahut paise aa gaye hain !!!");
        }
    }

	/*slips collected in game*/
    public void addSlips()
    {
		slips =  PlayerPrefs.GetInt("slips");
        slips++;
        PlayerPrefs.SetInt("slips", slips);
		slipText.text = " " + slips.ToString ();
		slipsCollectedInOneGame++;
		PlayerPrefs.SetInt("slipsCollectedInOneGame", slipsCollectedInOneGame);
		PlayerPrefs.Save();
    }


	//------ Created by RC-------//

	public void PauseGame()		 //--  pauses the game 
	{
		Time.timeScale =0;
		Debug.Log("Game Paused");
		ShowPauseMenu();
	}
	public void Play()		 //-- resumes the game 
	{
		Time.timeScale =1;
		Debug.Log("Game Resume");
		HidePauseMenu();
	}

	public void ShowPauseMenu()	//-- Show pause menu
	{
		gameMenu.transform.FindChild("Pause_Menu").gameObject.SetActive(true);
	}
	public void HidePauseMenu() //-- Hide pause menu
	{
		gameMenu.transform.FindChild("Pause_Menu").gameObject.SetActive(false);
	}

	public void ReloadScene() //-- Reloads the curret scene. It is used when the game is over
	{
		Application.LoadLevel(""+Application.loadedLevelName);
	}
	
	public void EndBack()
	{
		gameMenu.transform.FindChild("GameOver_Menu").gameObject.SetActive(true);
		gameMenu.transform.FindChild("Revive_Menu").gameObject.SetActive(false);
		gameMenu.transform.FindChild ("GameOver_Menu").gameObject.transform.FindChild ("High Score").gameObject.transform.FindChild ("HighScoreDisplay").GetComponent<Text> ().text = PlayerPrefs.GetFloat ("HighScore") + "";
	}

	void CalculateDistance()			 //RC -- Calculates the Distance
	{
		switch(PlayerPrefs.GetInt("BoatNumber"))
		{
			case 0: distanceXMultiplier.text = score.ToString () + " X 1.0" +  " = " + score*1.0f; CheckHighScore(score, 1.0f); break;
			case 1: distanceXMultiplier.text = score.ToString () + " X 1.2" +  " = " + score*1.2f;CheckHighScore(score, 1.2f);break;
			case 2: distanceXMultiplier.text = score.ToString () + " X 1.4" +  " = " + score*1.4f;CheckHighScore(score, 1.4f);break;
			case 3: distanceXMultiplier.text = score.ToString () + " X 1.6" +  " = " + score*1.6f;CheckHighScore(score, 1.6f);break;
			case 4: distanceXMultiplier.text = score.ToString () + " X 1.8" +  " = " + score*1.8f;CheckHighScore(score, 1.8f);break;
			case 5: distanceXMultiplier.text = score.ToString () + " X 2.0" +  " = " + score*2.0f;CheckHighScore(score, 2.0f);break;
			case 6: distanceXMultiplier.text = score.ToString () + " X 3.0" +  " = " + score*3.0f; CheckHighScore(score, 3.0f);break;
			default: distanceXMultiplier.text = score.ToString () + " X 1.2" +  " = " + score*1.2f; CheckHighScore(score, 1.2f);break;
		}

		if (score ==2) 
		{
			GameObject.Find ("enemyManager").GetComponent<enemyManagerScript> ().InitiateSpawn ();
			score = 3;
		}
	}

	void CheckHighScore(int score , float multiplier)			///--- RC --- To check high score and save it in "HIGHSCORE"
	{
		if(PlayerPrefs.GetFloat("HighScore") < score*multiplier)
		{
			PlayerPrefs.SetFloat("HighScore", score*multiplier);
		}
	}

	public void SubmitScore()
	{
		gameMenu.transform.FindChild("GameOver_Menu").gameObject.SetActive(false);
		SubmitScoreButton.gameObject.SetActive(true);
	}

	public void BackToSubmitScore()
	{
		SubmitScoreButton.gameObject.SetActive(false);
		gameMenu.transform.FindChild("GameOver_Menu").gameObject.SetActive(true);
	}

	public void InternetNotAvailable()
	{
		ui.transform.FindChild("Popup").gameObject.SetActive(true);
		ui.transform.FindChild("Main_Menu").gameObject.SetActive(false);
	}
	
	public void BackToMainMenu()
	{
		ui.transform.FindChild("Popup").gameObject.SetActive(false);
		ui.transform.FindChild("Main_Menu").gameObject.SetActive(true);
		ui.transform.FindChild("Boat_Menu").gameObject.SetActive(false);

	}

	public void ShowShopMenu()
	{
		ui.transform.FindChild("Shop_Menu").gameObject.SetActive(true);
	//	transform.FindChild("Score").gameObject.SetActive(false);
		ui.transform.FindChild("Main_Menu").gameObject.SetActive(false);
		ui.transform.FindChild("Boat_Menu").gameObject.SetActive(false);
	}

	public void HideShopMenu()
	{
		ui.transform.FindChild("Main_Menu").gameObject.SetActive(true);
	//	transform.FindChild("Score").gameObject.SetActive(true);
		ui.transform.FindChild("Shop_Menu").gameObject.SetActive(false);
	}

	public void ShowBoatMenu()
	{
		ui.transform.FindChild("Shop_Menu").gameObject.SetActive(false);
		ui.transform.FindChild("Boat_Menu").gameObject.SetActive(true);
	}
	
	public void HideBoatMenu()
	{
		ui.transform.FindChild("Boat_Menu").gameObject.SetActive(false);
		ui.transform.FindChild("Shop_Menu").gameObject.SetActive(true);
	}

	public void ShowThemeMenu()
	{
		ui.transform.FindChild("Shop_Menu").gameObject.SetActive(false);
		ui.transform.FindChild("Environment_Menu").gameObject.SetActive(true);
	}
	
	public void HideThemeMenu()
	{
		ui.transform.FindChild("Environment_Menu").gameObject.SetActive(false);
		ui.transform.FindChild("Shop_Menu").gameObject.SetActive(true);
	}
	
	public void ShowConfirmationMenu()
	{
		ui.transform.FindChild("Confirmation_Menu").gameObject.SetActive(true);
		ui.transform.FindChild("Boat_Menu").gameObject.SetActive(false);
	}

	public void HideConfirmationMenu()
	{

		 ui.transform.FindChild("Boat_Menu").gameObject.SetActive(true);
		 ui.transform.FindChild("Confirmation_Menu").gameObject.SetActive(false);
	}

	public void ShowExitConfirmationMenu()
	{
		ui.transform.FindChild("Main_Menu").gameObject.SetActive(false);
	//	transform.FindChild("Score").gameObject.SetActive(false);
		transform.FindChild("Slips").gameObject.SetActive(false);
		ui.transform.FindChild("Exit_Menu").gameObject.SetActive(true);
	}

//	public void HideExitConfirmationMenu()
//	{
//		//- ui.transform.FindChild("Exit_Menu").gameObject.SetActive(false);
//		//- transform.FindChild("Slips").gameObject.SetActive(true);
//		//- ui.transform.FindChild("Main_Menu").gameObject.SetActive(true);
//	}

	public void ShowInstructions()
	{


	}

		

	
}
