using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonControls : MonoBehaviour {

    public int number;
    public List<int> interactableButtons;
    public GameMaster gm;
    public string curChar;
	//me 
	public string st = "abcdefghijklmnopqrstuvwxyz";

	// Use this for initialization
	void Start () {
        //curChar = this.GetComponentInChildren<Text>().text;
		//me 
		curChar = st[Random.Range(0, st.Length)].ToString();
        gm = Camera.main.GetComponent<GameMaster>();
		changeCharacter (curChar);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void sendClickedMessage()
    {
        gm.hasButtonClicked[number] = true;
    }

    public void changeCharacter(string newChar)
    {
        curChar = newChar;
		Text currentText = this.GetComponentInChildren<Text>();
		currentText.text = newChar;
    }

}
