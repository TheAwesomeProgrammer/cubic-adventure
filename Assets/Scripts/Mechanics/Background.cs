using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public Sprite[] Backgrounds;

    private CupidHealth mPlayerHealth;

	// Use this for initialization
	void Start () {
        mPlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<CupidHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        SetBackgroundAccordingToPlayerLife();
	}

    void SetBackgroundAccordingToPlayerLife()
    {
        if (mPlayerHealth.cHealth == 3)
        {
            GetComponent<SpriteRenderer>().sprite = Backgrounds[0];
        }
        if (mPlayerHealth.cHealth == 2)
        {
            GetComponent<SpriteRenderer>().sprite = Backgrounds[1];
        }
        if (mPlayerHealth.cHealth == 1)
        {
            GetComponent<SpriteRenderer>().sprite = Backgrounds[2];
        }
    }
}
