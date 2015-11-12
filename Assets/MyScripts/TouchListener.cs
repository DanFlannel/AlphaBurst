using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

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

	// Use this for initialization
	void Start () {
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

        if (Input.touchCount > 0)
        {
            visibleDebug.text = "found a touch!";
            Touch t = Input.touches[0];
            isTouching = true;
            info = Input.touches[0];

            Vector3 screen_world = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosition = new Vector2(screen_world.x, screen_world.y);

            //listens for all interactions between the touch position and the box colliders
            for (int i = 0; i < colliderlist.Count; i++)
            {
                ButtonControls buttonInspector = buttonArray[i].GetComponent<ButtonControls>();
                Collider2D temp = colliderlist[i];
                if (temp == Physics2D.OverlapPoint(touchPosition))
                {
                    visibleDebug.text = "hit a button";
                    Debug.Log("hit button" + buttonArray[i].name);
                    if (curButton != -1)    //if this isnt the first button we have clicked
                    {
                        visibleDebug.text = "we have hit another button";
                        //check to make sure that the only buttons we interact with are ones that are linked
                        for (int j = 0; j < curInteractableButtons.Count; j++)
                        {
                            if (buttonInspector.number == curInteractableButtons[j])
                            {
                                charInput.text += buttonInspector.curChar;
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
            }
            if (t.phase == TouchPhase.Ended)
            { //if the touch has ended

                isTouching = false;
                charInput.text = "";
                //checkTheString
            }
        }
    }        
}

