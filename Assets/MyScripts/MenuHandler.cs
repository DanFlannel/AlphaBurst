using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class MenuHandler : MonoBehaviour {

    #region Declared Variables
    private bool signedIn;
    private bool firstSignIn;
    private bool usingLeaderboards;
    private bool signInSucess;
    public GameObject leaderBoardButton;

    public Text DebugText;

    public Button gameMode1;
    public Button gameMode2;
    public Button gameMode3;

    public Text highScore_1;
    public Text highScore_2;
    public Text highScore_3;
    #endregion

    // Use this for initialization
    void Start () {
        DebugText.text = "called start \n";
        Init();
        leaderBoardToggle();
        DebugText.text += "called init \n";
        signInCheck();
        DebugText.text += "checkd for sign in \n";
        leaderBoardToggle();
        DebugText.text += "rechecked leaderboard toggle\n";


        three_by_three();
        DebugText.text += "called three by three";
    }

    private void leaderBoardToggle()
    {
        if (!usingLeaderboards)
        {
            leaderBoardButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            leaderBoardButton.GetComponent<Button>().interactable = true;
        }
    }

    private void Init()
    {
        usingLeaderboards = PlayerPrefs.GetInt("leaderBoard") == 1;
        firstSignIn = PlayerPrefs.GetInt("FirstApplicationLoad") == 0;
        signInSucess = PlayerPrefs.GetInt("SignInSucess") == 1;
    }

    private void signInCheck()
    {
        DebugText.text += "first sign in: " + firstSignIn + " \n";
        DebugText.text += "sucessful signin before: " +  signInSucess + " \n";

        if (firstSignIn)    // if its the first time loading the app we will attempt to sign them in
        {
            signInMethod();
            PlayerPrefs.SetInt("FirstApplicationLoad", 1);  //this makes sure that we do not call this method again
        }

        else if (!firstSignIn && signInSucess)    //if this is no the first sign in but the first sign in was sucessful we will sign them in again
        {
            signInMethod();
        }
        else if (!firstSignIn & !signInSucess)       //if this is not the first sign in and their first sign in wasn't scuessful we will not try again
        {
            //we dont sign in because weve already tried that and the first time it didnt work
        }
    }

    public void signInMethod()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        DebugText.text = "";
        PlayerPrefs.SetInt("AttemptedToSignIn", 1);
        Social.localUser.Authenticate((bool sucess) =>
        {
            if (sucess)
            {
                //signIn.text = "Signed In";
                leaderBoardButton.GetComponent<Button>().interactable = true;
                DebugText.text += "You've sucessfully logged in! + \n";
                PlayerPrefs.SetInt("leaderBoard", 1);
                PlayerPrefs.SetInt("SignInSucess", 1);
            }
            else
            {
                //signIn.text = "Failed";
                leaderBoardButton.GetComponent<Button>().interactable = false;
                DebugText.text += "Failed to log in + \n";
                PlayerPrefs.SetInt("leaderBoard", 0);
            }
        });
    }
	
    public void three_by_three()
    {
        PlayerPrefs.SetString("current_level", "3x3");

        gameMode1.GetComponentInChildren<Text>().text = "30 Seconds";
        gameMode2.GetComponentInChildren<Text>().text = "1 Minute";
        gameMode3.GetComponentInChildren<Text>().text = "2 Minutes";

        highScore_1.text = PlayerPrefs.GetInt("3x3_30", 0).ToString();
        highScore_2.text = PlayerPrefs.GetInt("3x3_60", 0).ToString();
        highScore_3.text = PlayerPrefs.GetInt("3x3_120", 0).ToString();
    }

    public void four_by_four()
    {
        PlayerPrefs.SetString("current_level", "4x4");

        gameMode1.GetComponentInChildren<Text>().text = "1 Minute";
        gameMode2.GetComponentInChildren<Text>().text = "2 Minutes";
        gameMode3.GetComponentInChildren<Text>().text = "3 Minutes";

        highScore_1.text = PlayerPrefs.GetInt("4x4_60", 0).ToString();
        highScore_2.text = PlayerPrefs.GetInt("4x4_120", 0).ToString();
        highScore_3.text = PlayerPrefs.GetInt("4x4_180", 0).ToString();
    }

    public void loadLevel_with_time()
    {
        switch (PlayerPrefs.GetString("current_level"))
        {
            case "3x3":
                Application.LoadLevel("3x3");
                break;
            case "4x4":
                Application.LoadLevel("4x4");
                break;
        }
    }

    public void selectMode(int buttonNumber)
    {
        Debug.Log(PlayerPrefs.GetString("current_level"));
        switch (buttonNumber)
        {
            case 1:
                if(PlayerPrefs.GetString("current_level") == "3x3")
                {
                    PlayerPrefs.SetFloat("time", 30);
                }
                else
                {
                    PlayerPrefs.SetFloat("time", 60);
                }
                break;
            case 2:
                if (PlayerPrefs.GetString("current_level") == "3x3")
                {
                    PlayerPrefs.SetFloat("time", 60);
                }
                else
                {
                    PlayerPrefs.SetFloat("time", 120);
                }
                break;
            case 3:
                if (PlayerPrefs.GetString("current_level") == "3x3")
                {
                    PlayerPrefs.SetFloat("time", 120);
                }
                else
                {
                    PlayerPrefs.SetFloat("time", 180);
                }
                break;
        }
        loadLevel_with_time();
    }

    public void show_leaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}
