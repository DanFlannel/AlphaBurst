using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public List<bool> hasButtonClicked;

    public string alphabet = "abcdefghijklmnopqrstuvwxyz";
    public GameObject[] buttonArray;
    public List<Text> curLetters;

    public int vowles;
    public int constanants;

    public int vowelMin;
    public int vowelMax;

    void Awake()
    {
        vowelMin = set_VowelMin();
        vowelMax = set_VowelMax();
    }

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

    private int set_VowelMin()
    {
        string mode = PlayerPrefs.GetString("current_level");
        int min = 0;
        switch (mode)
        {
            case "3x3":
                min = 2;
                break;
            case "4x4":
                min = 3;
                break;
        }
        return min;
    }

    private int set_VowelMax()
    {
        string mode = PlayerPrefs.GetString("current_level");
        int max = 0;
        switch (mode)
        {
            case "3x3":
                max = 3;
                break;
            case "4x4":
                max = 5;
                break;
        }
        return max;
    }
}
