using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.IO;
using System.Text.RegularExpressions;

public class TouchListener : MonoBehaviour {

    private GameMaster gm;
    public int curButton = -1;      //our current button number
    public Text charInput;          //the text that the button letters will be added to
    public Text score;
    public int points;

    private List<int> curInteractableButtons = new List<int>();         //list of interactable buttons as integers
	private List<int> resetInteractions = new List<int>(new int[] {0,1,2,3,4,5,6,7,8}); //list of all buttons
    private List<int> curUsedButtons = new List<int>();

    public TextAsset dictionary;
    private string[] dictionaryLines;

    public bool isInGame = true;
    public bool hasTimeLeft = true;
    private timer timeScript;

	// Use this for initialization
	void Start () {
        timeScript = Camera.main.GetComponent<timer>();
        score.text = "0";
        points = 0;
        Input.simulateMouseWithTouches = true;              //makes the touches as if they were mouse touches
        charInput.text = "";                                //sets the input text to nothing
        gm = Camera.main.GetComponent<GameMaster>();
        dictionaryLines = Regex.Split(dictionary.text, "\n");
    }
	
	// Update is called once per frame
	void Update () {

        if(timeScript.timerText.text == "Times Up!")
        {
            hasTimeLeft = false;
        }

        if (Input.GetMouseButtonUp(0) || Input.touchCount > 1)
        {
            curInteractableButtons = resetInteractions; //reset the ineractions so when we touch again we can touch any buttons
            curButton = -1;                             //resets the button number interactions to -1 so we reset the touch listener
            
            bool isWord = check_Dictionary(charInput.text); //this checks to see if the letters are a word

            if (isWord) //this means that we have a legal word
            {
                int pointsToAdd = addScore(charInput.text);
                points += pointsToAdd;
                score.text = points.ToString();
                
                //this for loop changes all the used buttons to new random letters
                for(int i = 0; i < curUsedButtons.Count; i++)
                {
                    ButtonControls bc = fetchControls(curUsedButtons[i]);
                    int randomNum = UnityEngine.Random.Range(0, 103);
                    char newChar = bc.letterFrequency(randomNum);
                    bc.changeCharacter(newChar.ToString());
                }

            }
            curUsedButtons.Clear();
            charInput.text = "";                        //reset the text
        }
    }

    /// <summary>
    /// This method is called "on pointer over" the buttons. It handles all of the checking for input and changing variables for the next button
    /// </summary>
    public void checkTouch(int n)
    {
		if (!hasTimeLeft) { //if there is no time left we dont do anything
			return;
		}
		//resets our interaction and checks teh dictionary if we let go or have multiple fingers on the screen
        if (!Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) || Input.touchCount > 1)
        {
			curInteractableButtons = resetInteractions;
            return;
        }

        ButtonControls buttonInspector = fetchControls(n);
        if (curButton != -1)    //if this isnt the first button we have clicked
        {
            bool isUsed = false;
            //checks to make sure we havent used this button before
            for (int k = 0; k < curUsedButtons.Count; k++)
            {
                if (buttonInspector.number == curUsedButtons[k])    //if it is in this list we have used it before
                {
                    isUsed = true;
                }
            }
            if (!isUsed)    //if the buttons has not been used before for this word
            {
                //TODO change color in here!

                //check to make sure that the only buttons we interact with are ones that are linked
                for (int j = 0; j < curInteractableButtons.Count; j++)
                {
                    //checking for the correct button that we are now interacting with

                    if (buttonInspector.number == curInteractableButtons[j])
                    {
                        //we hit a button that we can touch
                        curUsedButtons.Add(buttonInspector.number);                     //adds the pressed button to a list of used buttons
                        charInput.text += buttonInspector.curChar;                      //add the text in the text box to our string
                        curButton = buttonInspector.number;                             //set our curButton number to the new one
                        curInteractableButtons = buttonInspector.interactableButtons;   //change what buttons we can now interact with
                    }
                }
            }
        }
        else
        {
            //TODO change color in here

            //this is the first button we have hit so we have to set everything
            curUsedButtons.Add(buttonInspector.number);
            curInteractableButtons = buttonInspector.interactableButtons;
            charInput.text = buttonInspector.curChar;
            curButton = buttonInspector.number;
            buttonInspector.sendClickedMessage();
        }
    }
    
    /// <summary>
    /// fetches the buttonControls script from the right button
    /// </summary>
    private ButtonControls fetchControls(int n)
    {
        ButtonControls bc;
        bc = GameObject.Find("Letter_" + n).GetComponent<ButtonControls>();
        return bc;
    } 
    
    /// <summary>
    /// checks the dictionary for the words being passed in
    /// </summary>
    private bool check_Dictionary(string word)
    {
        Debug.Log("called check dictionary");
        Debug.Log(word);
        Debug.Log("word length: " + word.Length);
        char[] wordChars = word.ToCharArray();
        Debug.Log(wordChars.ToString());
        for(int i = 0; i < dictionaryLines.Length; i++)
        {
            //check for length
            if (dictionaryLines[i].Length != word.Length + 1)   //not the same length
            {
                //wrong!
            }
            else    //they are the same length
            {
                //checking the first characters of the words
                string lower = dictionaryLines[i].ToLower();
                //Debug.Log(lower);
                char[] temp = lower.ToCharArray();
                if(temp[0] == wordChars[0])     //the words have the same first character and length
                {
                    int count = 0;
                    for(int j = 0; j < word.Length; j++)
                    {
                        if(temp[j] != word[j])  //if the letters dont match stop checking the loop
                        {
                            break;
                        }
                        else
                        {
                            count++;
                        }
                    }
                    if(count == word.Length)
                    {
                        Debug.Log("found :" + word + " equalling " + dictionaryLines[i]);
                        return true;
                    }
                }
                else //the words have different first characters
                {
                    
                }
            }
        }
        return false;
    }     

    /// <summary>
    /// simple factorial score adder
    /// </summary>
    private int addScore(string word)
    {
        int score = 0;
        int length = word.Length;
        for(int i = 0; i < word.Length; i++)
        {
            score += (i + 1) * 10;
        }
        return score;
    }
}

