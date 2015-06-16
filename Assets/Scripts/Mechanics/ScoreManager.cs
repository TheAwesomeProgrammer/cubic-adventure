using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public static int sScore;

    public GUIText GUIText;

	// Use this for initialization
	void Start () {
        sScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
        WriteScore();
	}

    void WriteScore()
    {
        GUIText.GetComponent<GUIText>().text = "Score : " + sScore;
    }

   
}
