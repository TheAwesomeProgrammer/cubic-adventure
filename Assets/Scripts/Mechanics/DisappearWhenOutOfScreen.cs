using UnityEngine;
using System.Collections;

public class DisappearWhenOutOfScreen : MonoBehaviour {

    public float HowLongToBeOutOfScreen = 2;

    private float mNextTimeToSelfDestroy;

    private bool mWasVisibleLastTime;

	// Use this for initialization
	void Start () {
        mWasVisibleLastTime = true;
	}
	
	// Update is called once per frame
	void Update () {
        ShouldSetSelfDestroyTimer();
        ShouldSelfDestroy();
	}

    void ShouldSetSelfDestroyTimer()
    {
        if (!GetComponent<Renderer>().isVisible && mWasVisibleLastTime != GetComponent<Renderer>().isVisible)
        {
            mNextTimeToSelfDestroy = Time.time + HowLongToBeOutOfScreen;
            mWasVisibleLastTime = GetComponent<Renderer>().isVisible;
        }
        else if (GetComponent<Renderer>().isVisible)
        {
            mNextTimeToSelfDestroy = float.MaxValue;
            mWasVisibleLastTime = GetComponent<Renderer>().isVisible;
        }
    }

    void ShouldSelfDestroy()
    {
        if (mNextTimeToSelfDestroy < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
