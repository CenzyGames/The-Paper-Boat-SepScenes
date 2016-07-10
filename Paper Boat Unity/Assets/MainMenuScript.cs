using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms;

public class MainMenuScript : MonoBehaviour {

	public GameObject ui;
	IAchievement achievement ;


	void Awake()
	{
		PlayGamesPlatform.Activate();	
	}

	// Use this for initialization
	void Start () 
	{
		AutoLogin();
		EnableAdvertisement();
		GooglePlayServicesInitiate();
		AchievementInitiate();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}


	void AutoLogin()
	{
		if (PlayerPrefs.HasKey("LoggedIn4") == false) 
		{
			PlayerPrefs.SetString ("LoggedIn4", "true");
			Social.localUser.Authenticate (success => {
				if (success) 
				{
					Social.ReportScore( (int)PlayerPrefs.GetFloat("HighScore"), "CgkIgr6bkeMCEAIQDg", (succes) => {							// handle success or failure
					});
				}
			});
		}
	}

	void EnableAdvertisement()
	{
		Advertisement.Initialize("1023219", true);
	}

	void GooglePlayServicesInitiate()
	{
		/*google play services setup*/
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
	}

	void AchievementInitiate()
	{
		achievement = Social.CreateAchievement();
	}

	public void login()
	{
		Social.Active.localUser.Authenticate(returnLogin); // google play login
		Social.ReportScore( (int)PlayerPrefs.GetFloat("HighScore"), "CgkIgr6bkeMCEAIQDg", (succes) => {							// handle success or failure
		});
		Debug.Log ("LOGIN called");
	}
	
	void returnLogin(bool success)
	{
		if (success)
		{
			//message = loggedIn;
		}
		else
		{
			Debug.Log("Success-------" + success);	
			InternetNotAvailable();
		}
	}

	public void logout()
	{
		((PlayGamesPlatform)Social.Active).SignOut ();
		PlayerPrefs.DeleteKey ("LoggedIn4");
		Debug.Log("Logout");
	}

	public void showAchievements()// RC ----- to check player is logged in and then only show achievements
	{
		Social.localUser.Authenticate (success => {
			if (success) {
				Social.Active.ShowAchievementsUI();			// Originally only this line was present
			}
			else
				InternetNotAvailable();
		});
	}
	
	public void showLeaderboard()
	{	
		Social.localUser.Authenticate (success => {
			if (success) {
				Social.Active.ShowLeaderboardUI();			// Originally only this line was present
			}
			else
				InternetNotAvailable();
		});
	}

	public void InternetNotAvailable()
	{
		ui.transform.FindChild("Popup").gameObject.SetActive(true);
		ui.transform.FindChild("Main_Menu").gameObject.SetActive(false);
	}

}
