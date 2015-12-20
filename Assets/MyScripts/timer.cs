using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// This class handles the countdown and display of the timer GUI
/// </summary>

public class timer: MonoBehaviour {
	
	int seconds = 0;
    private string mode;
    private float gameModeTime;
	public float startingTime = 120f;
	public Text timerText;
    public bool isPaused = true;
    public bool isGameOver = false;
    public TouchListener tc;

	void Start(){
        gameModeTime = PlayerPrefs.GetFloat("time");
        mode = PlayerPrefs.GetString("current_level");
        Debug.Log(mode + "_" + gameModeTime);
        startingTime = gameModeTime;
        isPaused = true;
        tc = Camera.main.GetComponent<TouchListener>();
		seconds = (int)startingTime %60;
		if (seconds < 10)
			timerText.text = (int)startingTime / 60 + ":0" + seconds;
		else {
			timerText.text = (int)startingTime/60 + ":" + seconds;
		}
	}

	void Update(){
        if (!isPaused && !isGameOver)
        {
            countdownTimer();
        }
        if (isGameOver)
        {
            setScore();
        }
	}

    private void countdownTimer()
    {
        if (startingTime > 0)
        {
            startingTime -= Time.deltaTime;
            seconds = (int)startingTime % 60;
            if (seconds < 10)
                timerText.text = (int)startingTime / 60 + ":0" + seconds;
            else
            {
                timerText.text = (int)startingTime / 60 + ":" + seconds;
            }
        }
        else
        {
            timerText.text = "Times Up!";
            isGameOver = true;
        }
    }

    private void restartTimer()
    {
        
        isPaused = false;
        isGameOver = false;
        startingTime = PlayerPrefs.GetFloat("time");
    }

    private void resetButtons()
    {
        for(int i = 0; i < tc.resetInteractions.Count; i++)
        {
            ButtonControls bc = tc.fetchControls(i);
            int randomNum = UnityEngine.Random.Range(0, 103);
            char newChar = bc.letterFrequency(randomNum);
            bc.changeCharacter(newChar.ToString());
        }
    }

    private void resetScore()
    {
        tc.points = 0;
        tc.score.text = tc.points.ToString();
    }

    public void setScore()
    {
        //future reference to set the high score
        if (PlayerPrefs.GetInt(mode + "_" + gameModeTime) < tc.points)
        {
            if (PlayerPrefs.GetInt("leaderBoard") == 1)
            {
                setLeaderBoard();
            }
            PlayerPrefs.SetInt(mode + "_" + gameModeTime, tc.points);
        }
    }

    private void setLeaderBoard()
    {
        string board = mode + "_" + gameModeTime;
        switch (board)
        {
            case "3x3_30":
                Social.ReportScore(tc.points, gpg_constants.leaderboard_3x3_30_seconds, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });
                break;
            case "3x3_60":
                Social.ReportScore(tc.points, gpg_constants.leaderboard_3x3_1_minute, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });
                break;
            case "3x3_120":
                Social.ReportScore(tc.points, gpg_constants.leaderboard_3x3_2_minutes, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });
                break;
            case "4x4_60":
                Social.ReportScore(tc.points, gpg_constants.leaderboard_4x4_1_minute, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });
                break;
            case "4x4_120":
                Social.ReportScore(tc.points, gpg_constants.leaderboard_4x4_2_minutes, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });
                break;
            case "4x4_180":
                Social.ReportScore(tc.points, gpg_constants.leaderboard_4x4_3_minutes, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });
                break;
        }
    }

    public void pauseButton()
    {
        Debug.LogWarning("pressed pause");
        isPaused = true;
    }

    public void playButton()
    {
        Debug.LogWarning("pressed play");
        if(startingTime > 0 && isPaused)
        {
            isPaused = false;
        }else if (startingTime <= 0)
        {
            restartTimer();
            resetButtons();
            resetScore();
        }
    }

    public void menu()
    {
        Application.LoadLevel("menu");
    }

}