using UnityEngine;
using System.Collections;

public class PlayBackgroundMusic : MonoBehaviour {

    private CupidHealth mCupidHealth;

	// Use this for initialization
	void Start () {
        mCupidHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<CupidHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        SetBackgroundMusic();
	}

    void SetBackgroundMusic()
    {
        if (mCupidHealth.cHealth == 3)
        {
            GameObject tChild = transform.FindChild("Level1").gameObject;
            if (!tChild.GetComponent<AudioSource>().isPlaying)
            {
                tChild.GetComponent<AudioSource>().Play();
                transform.FindChild("Level2").GetComponent<AudioSource>().Stop();
                transform.FindChild("Level3").GetComponent<AudioSource>().Stop();
            }
        }
        else  if (mCupidHealth.cHealth == 2)
        {
             GameObject tChild = transform.FindChild("Level2").gameObject;
             if (!tChild.GetComponent<AudioSource>().isPlaying)
             {
                 transform.FindChild("Level1").GetComponent<AudioSource>().Stop();
                 tChild.GetComponent<AudioSource>().Play();
                 transform.FindChild("Level3").GetComponent<AudioSource>().Stop();
             }
        }
        else if (mCupidHealth.cHealth == 1)
        {
             GameObject tChild = transform.FindChild("Level3").gameObject;
            if (!tChild.GetComponent<AudioSource>().isPlaying)
            {
            transform.FindChild("Level1").GetComponent<AudioSource>().Stop();
            transform.FindChild("Level2").GetComponent<AudioSource>().Stop();
            tChild.GetComponent<AudioSource>().Play();
            }
        }
        else if (mCupidHealth.cHealth == 0)
        {
            GameObject tChild = transform.FindChild("Level3").gameObject;
            if (tChild.GetComponent<AudioSource>().isPlaying)
            {
                transform.FindChild("Level1").GetComponent<AudioSource>().Stop();
                transform.FindChild("Level2").GetComponent<AudioSource>().Stop();
                tChild.GetComponent<AudioSource>().Stop();
            }
        }
    }
}
