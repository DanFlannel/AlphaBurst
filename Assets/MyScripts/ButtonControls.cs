using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonControls : MonoBehaviour {

    public int number;
    public List<int> interactableButtons;
    public GameMaster gm;
    public string curChar;
    public int randomNum;
	//me 
	public string st = "abcdefghijklmnopqrstuvwxyz";

	// Use this for initialization
	void Start () {
        //curChar = this.GetComponentInChildren<Text>().text;
        //me 
        randomNum = Random.Range(0, 103);
        //curChar = st[Random.Range(0, st.Length)].ToString();
        curChar = letterFrequency(randomNum).ToString();
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

    //gets letters based off approximate letter frequency
    public char letterFrequency(int n)
    {
        char c = 'e';

        if(n>= 0 && n <= 8)
        {
            c = 'a';
        }else if (n > 8 && n <= 10)
        {
            c = 'b';
        }else if (n > 10 && n <= 13)
        {
            c = 'c';
        }else if(n>13 && n <= 17)
        {
            c = 'd';
        }
        else if (n > 17 && n <= 29)
        {
            c = 'e';
        }
        else if (n > 29 && n <= 31)
        {
            c = 'f';
        }
        else if (n > 31 && n <= 33)
        {
            c = 'g';
        }
        else if (n > 33 && n <= 39)
        {
            c = 'h';
        }
        else if (n > 39 && n <= 46)
        {
            c = 'i';
        }
        else if (n > 46 && n <= 47)
        {
            c = 'j';
        }
        else if (n > 47 && n <= 48)
        {
            c = 'k';
        }
        else if (n > 48 && n <= 52)
        {
            c = 'l';
        }
        else if (n > 52 && n <= 54)
        {
            c = 'm';
        }
        else if (n > 54 && n <= 61)
        {
            c = 'n';
        }
        else if (n > 61 && n <= 69)
        {
            c = 'o';
        }
        else if (n > 69 && n <= 71)
        {
            c = 'p';
        }
        else if (n > 71 && n <= 72)
        {
            c = 'q';
        }
        else if (n > 72 && n <= 78)
        {
            c = 'r';
        }
        else if (n > 78 && n <= 84)
        {
            c = 's';
        }
        else if (n > 84 && n <= 93)
        {
            c = 't';
        }
        else if (n > 93 && n <= 96)
        {
            c = 'u';
        }
        else if (n > 96 && n <= 97)
        {
            c = 'v';
        }
        else if (n > 97 && n <= 99)
        {
            c = 'w';
        }
        else if (n > 99 && n <= 100)
        {
            c = 'x';
        }
        else if (n > 100 && n <= 102)
        {
            c = 'y';
        }
        else
        {
            c = 'z';
        }


        return c;
    }

}
