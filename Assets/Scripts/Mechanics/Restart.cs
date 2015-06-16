using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        InputCheck();
	}

    void InputCheck()
    {
        if (Input.GetButtonDown("RestartButton"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Menu");
        }
    }
}
