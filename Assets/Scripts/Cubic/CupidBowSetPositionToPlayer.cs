using UnityEngine;
using System.Collections;

public class CupidBowSetPositionToPlayer : MonoBehaviour {

    public Vector3 OffsetRight;
    public Vector3 OffsetLeft;

    protected Vector3 mOffset;

    protected Transform mPlayer;

	// Use this for initialization
	protected virtual void Start () {
        mPlayer = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        SetPosition();
	}

    public virtual void Setoffset(Direction pDirection)
    {
        if (pDirection == Direction.Left)
        {
            mOffset = OffsetLeft;
            Vector3 tLocalScale = transform.localScale;
            if (tLocalScale.x > 0)
            {
                tLocalScale.x *= -1;
            }

            transform.localScale = tLocalScale;

        }
        else if (pDirection == Direction.Right)
        {
            mOffset = OffsetRight;
            Vector3 tLocalScale = transform.localScale;
            if (tLocalScale.x < 0)
            {
                tLocalScale.x *= -1;
            }

            transform.localScale = tLocalScale;
        }
    }

    public void SetRotation(Vector3 pEulerAngels)
    {
        GetComponent<Renderer>().enabled = false;
    }

    protected virtual void SetPosition()
    {
        GetComponent<Renderer>().enabled = true;
        Vector3 tWantedPosition = mPlayer.position + mOffset;

        transform.position = tWantedPosition;
    }
}
