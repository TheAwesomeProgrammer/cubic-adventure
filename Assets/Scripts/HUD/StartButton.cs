using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    public float ScaleBy;
    public float ScaleTime;

    private Vector3 mStartScale;

	// Use this for initialization
	void Start () {
        mStartScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Application.LoadLevel("MainScene");
    }

    void OnMouseOver()
    {
        iTween.ScaleTo(gameObject, mStartScale + new Vector3(ScaleBy, ScaleBy, 0), ScaleTime);
    }

    void OnMouseExit()
    {
        iTween.ScaleTo(gameObject, mStartScale,ScaleTime);
    }
}
