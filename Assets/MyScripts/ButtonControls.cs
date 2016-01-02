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
    private bool start = true;

	// Use this for initialization
	void Start () {
        //curChar = this.GetComponentInChildren<Text>().text;
        //me 
        start = true;
        //curChar = st[Random.Range(0, st.Length)].ToString();
        curChar = letterFrequency().ToString();
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
                
                newChar = letterFrequency().ToString();
            }
            

        }
        else if(gm.vowles >= gm.vowelMax)  //too many vowels
        {
            while (checkCharacter(newChar) != 1)
            {
                newChar = letterFrequency().ToString();
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
    public char letterFrequency()
    {
        int n = Random.Range(0,  182303);
        char c = 'e';

        if(n>= 0 && n <= 21912)
        {
            c = 'e';
        }else if (n > 21912 && n <= 38499)
        {
            c = 't';
        }else if (n > 38499 && n <= 53309)
        {
            c = 'a';
        }else if(n > 53309 && n <= 67312)
        {
            c = 'o';
        }
        else if (n > 67312 && n <= 80630)
        {
            c = 'i';
        }
        else if (n > 80603 && n <= 93296)
        {
            c = 'n';
        }
        else if (n > 93296 && n <= 104746)
        {
            c = 's';
        }
        else if (n > 104746 && n <= 115723)
        {
            c = 'r';
        }
        else if (n > 115723 && n <= 12518)
        {
            c = 'h';
        }
        else if (n > 12518 && n <= 134392)
        {
            c = 'd';
        }
        else if (n > 134392 && n <= 141645)
        {
            c = 'l';
        }
        else if (n > 141645 && n <= 146891)
        {
            c = 'u';
        }
        else if (n > 146891 && n <= 151834)
        {
            c = 'c';
        }
        else if (n > 151834 && n <= 156595)
        {
            c = 'm';
        }
        else if (n > 156595 && n <= 160795)
        {
            c = 'f';
        }
        else if (n > 160795 && n <= 164648)
        {
            c = 'y';
        }
        else if (n > 164648 && n <= 168467)
        {
            c = 'w';
        }
        else if (n > 168467 && n <= 172160)
        {
            c = 'g';
        }
        else if (n > 172160 && n <= 175476)
        {
            c = 'p';
        }
        else if (n > 175476 && n <= 178191)
        {
            c = 'b';
        }
        else if (n > 178191 && n <= 180210)
        {
            c = 'v';
        }
        else if (n > 180210 && n <= 181467)
        {
            c = 'k';
        }
        else if (n > 181467 && n <= 181782)
        {
            c = 'x';
        }
        else if (n > 181782 && n <= 181987)
        {
            c = 'q';
        }
        else if (n > 181987 && n <= 182175)
        {
            c = 'j';
        }
        else if( n > 182175 && n <= 182303)
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
            return 0;
        }
        else if (System.Char.IsLetter(curChar))  //if the character is a constanat
        {
            return 1;
        }
        else
        {
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
