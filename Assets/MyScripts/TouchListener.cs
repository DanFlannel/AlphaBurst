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
        //Input.simulateMouseWithTouches = true;
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
                checkTouch(Input.touches[0].position);
            }
            else
            {
                visibleDebug.text = Input.touchCount.ToString();
            }

        }else if(device == RuntimePlatform.WindowsEditor || device == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                checkTouch(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                visibleDebug.text = Input.touchCount.ToString();
            }
        }
    }
    
    private void checkTouch(Vector2 pos)
    {
        visibleDebug.text = "found a touch!";
        isTouching = true;

        Vector3 screen_world = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPosition = new Vector2(screen_world.x, screen_world.y);

        ButtonControls buttonInspector;
        Collider2D hitColl;

        RaycastHit2D rayHit = Physics2D.Raycast(touchPosition, Vector2.zero);

        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);
        }

        if (rayHit)
        {
            Debug.Log(rayHit.collider.name);
        }

        if (Physics2D.OverlapPoint(touchPosition,5))
        {
            hitColl = Physics2D.OverlapPoint(touchPosition);
            buttonInspector = hitColl.GetComponent<ButtonControls>();

            visibleDebug.text = "hit a button";
            //Debug.Log("hit button" + hit.name);
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
        /*if (t.phase == TouchPhase.Ended)
        { //if the touch has ended

            isTouching = false;
            charInput.text = "";
            //checkTheString
        }*/
    }
    
    private void UITouchDetect(Touch touch)
    {
        EventSystem eventSystem = EventSystem.current;
        PointerEventData eventData;
        int pointerID = touch.fingerId;
        List<RaycastResult> rr = new List<RaycastResult>();
        /*if (EventSystem.current.RaycastAll(eventData, rr)){
            Debug.Log(EventSystem.current.)
        }*/
    }       
}

