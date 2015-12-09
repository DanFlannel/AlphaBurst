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
    private bool start = true;

	// Use this for initialization
	void Start () {
        //curChar = this.GetComponentInChildren<Text>().text;
        //me 
        start = true;
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

    /// <summary>
    /// Called at the start and when we are changing letters because we made a word
    /// </summary>
    /// <param name="newChar">a new character that will replace the old one</param>
    public void changeCharacter(string newChar)
    {
        int type2;
        if (!start)
        {
            int type = checkCharacter(curChar);
            adjustLetterValues(type, false);
        }
        
        
        if(gm.vowles < gm.vowelMin)     //not enough vowels
        {
            //we need this to be a vowel
            while(checkCharacter(newChar) != 0)
            {
                int rnd = Random.Range(0, 103);
                newChar = letterFrequency(rnd).ToString();
            }
            

        }
        else if(gm.vowles >= gm.vowelMax)  //too many vowels
        {
            while (checkCharacter(newChar) != 1)
            {
                int rnd = Random.Range(0, 103);
                newChar = letterFrequency(rnd).ToString();
            }
        }
        else { 
            //just right
        }

        start = false;
        type2 = checkCharacter(newChar);
        adjustLetterValues(type2, true);

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

    private int checkCharacter(string letter)
    {
        char curChar;
        char[] temp = letter.ToCharArray();
        curChar = temp[0];

        if (curChar == 'a' || curChar == 'e' || curChar == 'i' || curChar == 'o' || curChar == 'u')  //if the character is a vowel
        {
            Debug.Log("Vowel");
            return 0;
        }
        else if (System.Char.IsLetter(curChar))  //if the character is a constanat
        {
            Debug.Log("Non Vowel");
            return 1;
        }
        else
        {
            Debug.Log("Number or non character letter");
            return 2;   //if the character is a number
        }
    }

    private void adjustLetterValues(int n, bool add)
    {
        switch (n)
        {
            case 0:     //vowel
                if (add)
                {
                    gm.vowles++;
                }
                else
                {
                    gm.vowles--;
                }
                break;
            case 1:     //non vowel
                if (add)
                {
                    gm.constanants++;
                }
                else
                {
                    gm.constanants--;
                }
                break;
            case 2:     //number
                break;
        }
    }

}
