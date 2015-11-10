using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchListener : MonoBehaviour {

    private GameObject[] buttonArray;
    private List<Collider2D> colliderlist = new List<Collider2D>();

	// Use this for initialization
	void Start () {
        buttonArray = GameObject.FindGameObjectsWithTag("Button");
        for(int i = 0; i < buttonArray.Length; i++)
        {
            colliderlist.Add(buttonArray[i].GetComponent<Collider2D>());
        }
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.touchCount == 1)
        {
            Vector3 screen_world = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosition = new Vector2(screen_world.x, screen_world.y);

            //listens for all interactions between the touch position and the box colliders
            for(int i = 0; i < colliderlist.Count; i++)
            {
                Collider2D temp = colliderlist[i];
                if (temp == Physics2D.OverlapPoint(touchPosition))
                {
                    Debug.Log("hit button" + buttonArray[i].name);
                }
            }

        }else if(Input.touchCount > 1)
        {
            //more than 1 touch
        }
        else
        {
            //no touch
        }

	}
}
