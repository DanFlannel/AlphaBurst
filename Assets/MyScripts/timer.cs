using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer: MonoBehaviour {
	
	int seconds = 0;
	public float startingTime = 120f;
	public Text timerText;

	void Start(){
		seconds = (int)startingTime %60;
		if (seconds < 10)
			timerText.text = (int)startingTime / 60 + ":0" + seconds;
		else {
			timerText.text = (int)startingTime/60 + ":" + seconds;
		}
	}

	void Update(){
		if (startingTime > 0) {
			startingTime -= Time.deltaTime;
			seconds = (int)startingTime % 60;
			if (seconds < 10)
				timerText.text = (int)startingTime / 60 + ":0" + seconds;
			else {
				timerText.text = (int)startingTime / 60 + ":" + seconds;
			}
		} else {
			timerText.text = "Times Up!";
		}
	}

}