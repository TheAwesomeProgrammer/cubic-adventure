using UnityEngine;
using System.Collections;

public class HeartParticlesFollow : MonoBehaviour {

    public Vector3 Offset;

    private Transform mTarget;

    private bool mHasHadATarget;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ShouldSetPosition();
	}

    void ShouldSetPosition()
    {
        if (mTarget != null)
        {
            mHasHadATarget = true;
            SetPosition();
        }
        if (mHasHadATarget && mTarget == null)
        {
            Destroy(gameObject);
        }
    }

    void SetPosition()
    {
        Vector3 tWantedPos = mTarget.position + Offset;

        transform.position = tWantedPos;
    }


    public void SetTarget(GameObject pTarget)
    {
        mTarget = pTarget.transform;
    }

    public void DeleteTarget()
    {
        mTarget = null;
    }
}
