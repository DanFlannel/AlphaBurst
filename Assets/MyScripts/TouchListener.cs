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
    public Text visibleDebug;
    public List<int> curInteractableButtons = new List<int>();
    private RuntimePlatform device = Application.platform;

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
       

        if(device == RuntimePlatform.Android || device == RuntimePlatform.IPhonePlayer)
        {
            if(Input.touchCount > 0)
            {
                //checkTouch();
            }
            else
            {
                visibleDebug.text = Input.touchCount.ToString();
            }

        }else if(device == RuntimePlatform.WindowsEditor || device == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButton(0))
            {
                //checkTouch();                
            }
            if (Input.GetMouseButtonUp(0))
            {
                visibleDebug.text = Input.touchCount.ToString();
            }
        }
    }

    public void checkTouch(int n)
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        ButtonControls buttonInspector = fetchControlls(n);
        if (curButton != -1)    //if this isnt the first button we have clicked
        {
            visibleDebug.text = "we have hit another button";
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
            visibleDebug.text = "we hit our first button";
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

