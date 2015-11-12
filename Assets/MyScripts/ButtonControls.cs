using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonControls : MonoBehaviour {

    public int number;
    public List<int> interactableButtons;
    public GameMaster gm;
    public string curChar;

	// Use this for initialization
	void Start () {
        curChar = this.GetComponentInChildren<Text>().text;
        gm = Camera.main.GetComponent<GameMaster>();
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
    }

}
