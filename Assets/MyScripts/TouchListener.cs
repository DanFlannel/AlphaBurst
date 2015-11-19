using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class TouchListener : MonoBehaviour {

    private GameObject[] buttonArray;
    private GameMaster gm;
    private List<Collider2D> colliderlist = new List<Collider2D>();
    public int curButton = -1;
    public bool isTouching = false;
    private Touch info;
    public Text charInput;
    public List<int> curInteractableButtons = new List<int>();
    private RuntimePlatform device = Application.platform;
	private List<int> resetInteractions = new List<int>(new int[] {0,1,2,3,4,5,6,7,8});
	public bool isInGame = true;

	// Use this for initialization
	void Start () {
        Input.simulateMouseWithTouches = true;
        charInput.text = "";
        gm = Camera.main.GetComponent<GameMaster>();
        buttonArray = GameObject.FindGameObjectsWithTag("Button");
        for(int i = 0; i < buttonArray.Length; i++)
        {
            colliderlist.Add(buttonArray[i].GetComponent<Collider2D>());
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            curInteractableButtons = resetInteractions;
            charInput.text = "";
        }

        if(device == RuntimePlatform.Android || device == RuntimePlatform.IPhonePlayer)
        {
            if(Input.touchCount > 0)
            {
                //checkTouch();
            }
            else
            {

            }

        }else if(device == RuntimePlatform.WindowsEditor || device == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButton(0))
            {
                //checkTouch();                
            }
            if (Input.GetMouseButtonUp(0))
            {

            }
        }
    }

    public void checkTouch(int n)
    {
		if (!isInGame) {
			return;
		}
		//resets our interaction and checks teh dictionary if we let go or have multiple fingers on the screen
        if (!Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) || Input.touchCount > 1)
        {
			curInteractableButtons = resetInteractions;
            return;
        }

        ButtonControls buttonInspector = fetchControlls(n);
        if (curButton != -1)    //if this isnt the first button we have clicked
        {
            //check to make sure that the only buttons we interact with are ones that are linked
            for (int j = 0; j < curInteractableButtons.Count; j++)
            {
                if (buttonInspector.number == curInteractableButtons[j])
                {
                    charInput.text += buttonInspector.curChar;
                    curButton = buttonInspector.number;
                    curInteractableButtons = buttonInspector.interactableButtons;
                    //we hit a button that we can touch
                }
            }
        }
        else
        {
            curInteractableButtons = buttonInspector.interactableButtons;
            charInput.text = buttonInspector.curChar;
            curButton = buttonInspector.number;
            buttonInspector.sendClickedMessage();
        }
    }
    
    private ButtonControls fetchControlls(int n)
    {
        ButtonControls bc;
        bc = GameObject.Find("Letter_" + n).GetComponent<ButtonControls>();
        return bc;
    }      
}

