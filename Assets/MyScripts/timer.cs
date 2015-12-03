using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// This class handles the countdown and display of the timer GUI
/// </summary>

public class timer: MonoBehaviour {
	
	int seconds = 0;
	public float startingTime = 120f;
	public Text timerText;
    public bool isPaused = true;
    public bool isGameOver = false;
    public TouchListener tc;

	void Start(){
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

    private void restartTimer(int length)
    {
        isPaused = false;
        isGameOver = false;
        startingTime = length;
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
        //future reference to set the high score
        if (PlayerPrefs.GetInt("HighScore") < tc.points) {
            PlayerPrefs.SetInt("HighScore",tc.points);
        }
        tc.points = 0;
        tc.score.text = tc.points.ToString();
    }

    public void pauseButton()
    {
        Debug.LogWarning("pressed pause");
        isPaused = true;
    }

    public void playButton(int length)
    {
        Debug.LogWarning("pressed play");
        if(startingTime > 0 && isPaused)
        {
            isPaused = false;
        }else if (startingTime <= 0)
        {
            restartTimer(length);
            resetButtons();
            resetScore();
        }
    }

}