﻿using UnityEngine;
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

	public GameObject EnvironmentConfirmationMenu; 

	string message ="";	// RC --- Required to show message on the screen
	string internetNotconnected = "Internet not connected";
	string blank = "";
	string userNotLoggedIn = "User not Logged In";
	string loggedIn = "Player Logged In";

	IAchievement achievement ;

	int count =1;// RC --- to deduct slips in power of 2. Used in the method useslips()

	int slipsCollectedInOneGame =0;

	int[] boatPrice = {0,400, 900, 1500, 2200, 3000, 4000} ;



	void Awake()
	{
		PlayGamesPlatform.Activate();
		GameObject.Find ("Environment").GetComponent<EnvironmentManager> ().LoadEnviroment();
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
		if(slipText!=null)
		slipText.text = " " + slips.ToString ();
        
		/*sets up camera for ui*/
		move = true;
        moveTo = Camera.main.transform.position;

		message = blank;		// RC ----- to hide Internetcheck message

		PlayerPrefs.SetString("BoatRevived", "false");
		slipsCollectedInOneGame =0;
		PlayerPrefs.SetInt("slipsCollectedInOneGame", slipsCollectedInOneGame);

		boatSpawnIndex = PlayerPrefs.GetInt ("BoatNumber");
		if(Application.loadedLevelName == "GamePlay")
			playGame(boatSpawnIndex);
		if(EnvironmentConfirmationMenu!=null)
		EnvironmentConfirmationMenu.gameObject.SetActive(false);
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
								//RC---
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
		}
		if (move)
        {
            moveBy = Vector3.MoveTowards(Camera.main.transform.position, moveTo, 1);
            Camera.main.transform.position = moveBy;
        }
    }

	/*starts game*/
	public void playGame(int boatSpawnIndex)
    {
		Time.timeScale =1; 
		if(transform.FindChild("Score")!=null)
		transform.FindChild("Score").gameObject.SetActive(true);
		bridge.SetActive(false);
        move = false;
		/*Instantiates the selected boat*/
		PlayerPrefs.SetInt("BoatNumber", boatSpawnIndex);		//RC --- To save the boat number and then to calculate the score.

		//LOAD THE BOAT SPAWNINDEX FROM PLAYERPREFS TO INITIALIZE THE BOAT BUY
        
		Instantiate(boats[boatSpawnIndex], new Vector3(2.7f, 0.0f, -0.91f), Quaternion.identity);

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

//	public void gotoPanelBoatMenu(int xVal)
//	{
//		switch(xVal)
//		{
//		case 0: if(CheckSlipsValue(boatPrice[0])){	//gotoPanel(17.93f); 
//				ShowConfirmationMenu();}break;
//		case 1: if(CheckSlipsValue(boatPrice[1])){	//gotoPanel(17.93f); 
//				ShowConfirmationMenu();}break;
//		case 2: if(CheckSlipsValue(boatPrice[2])){	//gotoPanel(17.93f);
//				ShowConfirmationMenu();}break;
//		case 3: if(CheckSlipsValue(boatPrice[3])){	//gotoPanel(17.93f);
//				ShowConfirmationMenu();}break;
//		case 4: if(CheckSlipsValue(boatPrice[4])){	//gotoPanel(17.93f);
//				ShowConfirmationMenu();}break;
//		case 5: if(CheckSlipsValue(boatPrice[5])){	//gotoPanel(17.93f);
//				ShowConfirmationMenu();}break;
//		case 6: if(CheckSlipsValue(boatPrice[6])){	//gotoPanel(17.93f);
//				ShowConfirmationMenu();}break;
//			default : break;
//		} 
//			
//	}


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
		PlayerPrefs.SetInt ("BoatNumber", index);
		Debug.Log("Index" + index);
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
			case 0: PlayerPrefs.SetInt("slips", slips - boatPrice[0]); PlayerPrefs.SetInt("DefaultBoat", 0); break;
			case 1: PlayerPrefs.SetInt("slips", slips - boatPrice[1]); PlayerPrefs.SetInt("DefaultBoat", 1); break;
			case 2: PlayerPrefs.SetInt("slips", slips - boatPrice[2]); PlayerPrefs.SetInt("DefaultBoat", 2); break;
			case 3: PlayerPrefs.SetInt("slips", slips - boatPrice[3]); PlayerPrefs.SetInt("DefaultBoat", 3); break;
			case 4: PlayerPrefs.SetInt("slips", slips - boatPrice[4]); PlayerPrefs.SetInt("DefaultBoat", 4); break;
			case 5: PlayerPrefs.SetInt("slips", slips - boatPrice[5]); PlayerPrefs.SetInt("DefaultBoat", 5); break;
			case 6: PlayerPrefs.SetInt("slips", slips - boatPrice[6]); PlayerPrefs.SetInt("DefaultBoat", 6); break;
			default : break;
		}
			slips = PlayerPrefs.GetInt("slips");
			PlayerPrefs.Save();
			slipText.text = " " + slips.ToString ();
			HideNotEnoughSlips();
		Debug.Log("Index 2" + index);
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
		if(gameMenu!=null)
		gameMenu.transform.FindChild("Pause_Menu").gameObject.SetActive(true);
	}
	public void HidePauseMenu() //-- Hide pause menu
	{
		if(gameMenu!=null)
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
		case 0: distanceXMultiplier.text = score.ToString () + " X 1" +  " = " + score*boatMultiplier[0] ; CheckHighScore(score, boatMultiplier[0] ); break;
		case 1: distanceXMultiplier.text = score.ToString () + " X 2" +  " = " + score*boatMultiplier[1] ;CheckHighScore(score, boatMultiplier[1] );break;
		case 2: distanceXMultiplier.text = score.ToString () + " X 3" +  " = " + score*boatMultiplier[2] ;CheckHighScore(score, boatMultiplier[2] );break;
		case 3: distanceXMultiplier.text = score.ToString () + " X 4" +  " = " + score*boatMultiplier[3] ;CheckHighScore(score, boatMultiplier[3] );break;
		case 4: distanceXMultiplier.text = score.ToString () + " X 5" +  " = " + score*boatMultiplier[4] ;CheckHighScore(score, boatMultiplier[4] );break;
		case 5: distanceXMultiplier.text = score.ToString () + " X 6" +  " = " + score*boatMultiplier[5] ;CheckHighScore(score, boatMultiplier[5] );break;
		case 6: distanceXMultiplier.text = score.ToString () + " X 7" +  " = " + score*boatMultiplier[6] ; CheckHighScore(score, boatMultiplier[6] );break;
		default: distanceXMultiplier.text = score.ToString () + " X 1" +  " = " + score*boatMultiplier[7] ; CheckHighScore(score, boatMultiplier[0] );break;
		}
//
//		if (score ==2) 
//		{
//			GameObject.Find ("enemyManager").GetComponent<enemyManagerScript> ().InitiateSpawn ();
//			score = 3;
//		}
	}

	void CheckHighScore(int score , float multiplier)			///--- RC --- To check high score and save it in "HIGHSCORE"
	{
		PlayerPrefs.SetString ("CurrentScore", score +  " X "  + multiplier + " = " + score * multiplier);
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
		ui.transform.FindChild("Boat Confirmation_Menu").gameObject.SetActive(true);
		ui.transform.FindChild("Boat_Menu").gameObject.SetActive(false);
	}

	public void HideConfirmationMenu()
	{

		 ui.transform.FindChild("Boat_Menu").gameObject.SetActive(true);
		 ui.transform.FindChild("Boat Confirmation_Menu").gameObject.SetActive(false);
	}

	public void ShowExitConfirmationMenu()
	{
		ui.transform.FindChild("Main_Menu").gameObject.SetActive(false);
		transform.FindChild("Slips").gameObject.SetActive(false);
		ui.transform.FindChild("Exit_Menu").gameObject.SetActive(true);
	}


	public void ShowSlipsMenu()
	{
		ui.transform.FindChild("Slips_Menu").gameObject.SetActive(true);
		ui.transform.FindChild("Shop_Menu").gameObject.SetActive(false);
	}

	public void HideSlipsMenu()
	{
		ui.transform.FindChild("Slips_Menu").gameObject.SetActive(false);
		ui.transform.FindChild("Shop_Menu").gameObject.SetActive(true);	
	}

	public void ShowEnvironmentDefaultMenu()
	{
		ui.transform.FindChild("Environment_Menu").gameObject.SetActive(false);
		EnvironmentConfirmationMenu.gameObject.SetActive(true);
		EnvironmentConfirmationMenu.transform.FindChild("Spring").gameObject.SetActive(true);
		EnvironmentConfirmationMenu.transform.FindChild("Snow").gameObject.SetActive(false);
	}

	public void HideEnvironmentDefaultMenu()
	{
		ui.transform.FindChild("Environment_Menu").gameObject.SetActive(true);
		EnvironmentConfirmationMenu.gameObject.SetActive(false);
		EnvironmentConfirmationMenu.transform.FindChild("Spring").gameObject.SetActive(false);
		EnvironmentConfirmationMenu.transform.FindChild("Snow").gameObject.SetActive(false);
	}

	public void ShowEnvironmentSnowMenu()
	{
		ui.transform.FindChild("Environment_Menu").gameObject.SetActive(false);
		EnvironmentConfirmationMenu.gameObject.SetActive(true);
		EnvironmentConfirmationMenu.transform.FindChild("Snow").gameObject.SetActive(true);
		EnvironmentConfirmationMenu.transform.FindChild("Spring").gameObject.SetActive(false);
	}
	
	public void HideEnvironmentSnowMenu()
	{
		ui.transform.FindChild("Environment_Menu").gameObject.SetActive(true);
		EnvironmentConfirmationMenu.gameObject.SetActive(false);
		EnvironmentConfirmationMenu.transform.FindChild("Snow").gameObject.SetActive(false);
		EnvironmentConfirmationMenu.transform.FindChild("Spring").gameObject.SetActive(false);
	}
	public void ShowEnvironmentMenu()
	{
		ui.transform.FindChild("Environment_Menu").gameObject.SetActive(false);
		ui.transform.FindChild("Environment Confirmation_Menu").gameObject.SetActive(true);

		
	}
	public void HideEnvironmentMenu(int environment)
	{
		if(environment == 0 || environment == 1)
		{
		PlayerPrefs.SetInt ("environment", environment);
		}
		else
		{
			PlayerPrefs.SetInt ("environment", PlayerPrefs.GetInt("environment"));		// for 2 in inspector
		}
			ui.transform.FindChild("Environment_Menu").gameObject.SetActive(true);
		ui.transform.FindChild("Environment Confirmation_Menu").gameObject.SetActive(false);
	
	}

	
}
