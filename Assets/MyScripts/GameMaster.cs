using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public List<bool> hasButtonClicked;

    public string alphabet = "abcdefghijklmnopqrstuvwxyz";
    public GameObject[] buttonArray;
    public List<Text> curLetters;

	// Use this for initialization
	void Start () {
        buttonArray = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < buttonArray.Length; i++)
        {
            curLetters.Add(buttonArray[i].GetComponentInChildren<Text>());
            hasButtonClicked.Add(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
