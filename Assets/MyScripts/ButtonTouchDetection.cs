using UnityEngine;
using System.Collections;

public class ButtonTouchDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        Debug.Log("the mouse is over this");
    }

    public void test()
    {
        if(Input.GetMouseButton(0))
            Debug.Log("Working");
    }
}
