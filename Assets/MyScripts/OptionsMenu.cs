using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    public GameObject optionsMenu;
    public bool menuEnable;
    
	// Use this for initialization
	void Start () {
        Init();
	}

    void Init()
    {
        optionsMenu.gameObject.SetActive(false);
        menuEnable = false;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void toggleMenu()
    {
        menuEnable = !menuEnable;
        if (menuEnable)
        {
            optionsMenu.gameObject.SetActive(true);
        }
        else
        {
            optionsMenu.gameObject.SetActive(false);
        }
    }
}
