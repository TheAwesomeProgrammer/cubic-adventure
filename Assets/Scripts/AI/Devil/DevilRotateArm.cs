using UnityEngine;
using System.Collections;

public class DevilRotateArm : MonoBehaviour {

    public float MaxRotation;
    public float MinRotation;

    public Vector3 Offset;

    public float RotateSpeed;
    public GameObject Devil;

    private Transform mPlayer;

    private Vector2 mTarget;

    private DevilMovement mDevilMovement;

	// Use this for initialization
	void Start () {
        mPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        mDevilMovement = Devil.GetComponent<DevilMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        SetTarget();
        RotateTowardsPlayer();
	}

    void SetTarget()
    {
        mTarget = mPlayer.position + Offset;
    }

    void RotateTowardsPlayer()
    {
        float tAngle = CalculateAngle();
      


            Quaternion mRotation = Quaternion.identity;

            if (mDevilMovement.cMovingDirection == Direction.Left)
            {
                mRotation = Quaternion.Euler(0, 0, tAngle - 90);
            }
            else if (mDevilMovement.cMovingDirection == Direction.Right)
            {
                mRotation = Quaternion.Euler(0, 0, tAngle - 90);
            }



            transform.rotation = Quaternion.Slerp(transform.rotation, mRotation, Time.deltaTime * RotateSpeed);
        
        
    }

    float CalculateAngle()
    {
        float tAngle = 0;

        

        tAngle = (Mathf.Atan2(mTarget.y - transform.position.y, mTarget.x - transform.position.x) * Mathf.Rad2Deg);



        return tAngle;
    }

    public bool CanRotateToTheTarget(float pAngle)
    {
        bool tCanRotateToTarget = true;

        if (pAngle < 0)
        {
            pAngle += 360;

            if (MaxRotation > 180)
            {
                if (pAngle > MaxRotation)
                {

                    pAngle = transform.rotation.eulerAngles.z;
                    tCanRotateToTarget = false;
                }
            }
            if (MinRotation > 180)
            {
                if (pAngle < MinRotation)
                {

                    pAngle = transform.rotation.eulerAngles.z;
                    tCanRotateToTarget = false;
                }
            }
        }
        else
        {
            if (MaxRotation <= 180)
            {

                if (pAngle > MaxRotation)
                {
                    pAngle = transform.rotation.eulerAngles.z;
                    tCanRotateToTarget = false;
                }
            }
            if (MinRotation <= 180)
            {
                if (pAngle < MinRotation)
                {
                    pAngle = transform.rotation.eulerAngles.z;
                    tCanRotateToTarget = false;
                }
            }
        }

        return tCanRotateToTarget;
    }
}
