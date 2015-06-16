using UnityEngine;
using System.Collections.Generic;

public enum State
{
    Start,
    Stop
}


public class AIMovement : MonoBehaviour
{

    public Vector2[] DirectionVectorsToCheckFor = 
    {
        new Vector2(0,1),
        new Vector2(1,0),
        new Vector2(-1,0),
        new Vector2(0,-1),
        new Vector2(1,-1),
        new Vector2(1,1),
        new Vector2(-1,1),
        new Vector2(-1,-1),

    };
    public float CheckForNpcsDistance = 0.25f;
    public float RadiusCheck;

    public Vector2 SpeedMaxMinInterval = new Vector2(0.5f,2);
    public Vector2 StopMaxMinInterval = new Vector2(1, 5);
    public Vector2 StartMaxMinInterval = new Vector2(2, 3);
    public Vector2 ChangePathMaxMinInterval = new Vector2(20, 70);

    public float DistanceToBeToReachTarget = 0.25f;

    protected Vector2 mDirection;

    protected State mMovingState;

    protected float mNextTimeToStop;
    protected float mNextTimeToStart;

    protected Animator mAnimator;

    protected GameObject mCallbackObject;
    protected GameObject mTargetToFollow;

    protected bool mBlock;

	// Use this for initialization
	protected virtual void Start () {
        StartMoving();
        mAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {

            ShouldStart();
            ShouldStop();
            Move();
        
        if (mTargetToFollow)
        {
        CalculateDirectionToTarget();
        HasReachedTarget();
        Move();
        }
	}

    public virtual void FollowATargetAndCallBack(GameObject pCallbackObject,GameObject pTargetToFollow)
    {
        mCallbackObject = pCallbackObject;
        mTargetToFollow = pTargetToFollow;
        mNextTimeToStart = float.MaxValue;
        mNextTimeToStop = float.MaxValue;
    }

    public virtual void FollowATarget(GameObject pTargetToFollow)
    {
        mTargetToFollow = pTargetToFollow;
        mNextTimeToStart = float.MaxValue;
        mNextTimeToStop = float.MaxValue;
    }

    protected virtual void CalculateDirectionToTarget()
    {
        mDirection = (mTargetToFollow.transform.position - transform.position).normalized * Random.Range(SpeedMaxMinInterval.x, SpeedMaxMinInterval.y);
    }



    protected virtual void HasReachedTarget()
    {
        if (Vector2.Distance(transform.position, mTargetToFollow.transform.position) < DistanceToBeToReachTarget)
        {
            if (mCallbackObject)
            {
                mCallbackObject.SendMessage("Callback",gameObject);
            }
            Reset();
        }
    }

    protected virtual void Reset()
    {
        StartMoving();
        mCallbackObject = null;
        mTargetToFollow = null;
    }

    protected virtual void CalculateStopTime()
    {
        mNextTimeToStop = Time.time + Random.Range(StopMaxMinInterval.x,StopMaxMinInterval.y);
    }

    protected virtual void CalculateStartTime()
    {
        mNextTimeToStart = Time.time + Random.Range(StartMaxMinInterval.x, StartMaxMinInterval.y);
    }


    protected virtual Vector2 CalculateRandomDirection()
    {
        Vector2 tRandomRotation = Vector2.zero;

        float tRandomNumber = Random.Range(0f, 1f);

        if (tRandomNumber > 0.5f)
        {
            tRandomRotation = new Vector2(Random.Range(SpeedMaxMinInterval.x, SpeedMaxMinInterval.y),
                                          Random.Range(SpeedMaxMinInterval.x, SpeedMaxMinInterval.y));
        }
        else if (tRandomNumber < 0.5f)
        {
            tRandomRotation = new Vector2(Random.Range(-SpeedMaxMinInterval.x, -SpeedMaxMinInterval.y),
                                         Random.Range(-SpeedMaxMinInterval.x, -SpeedMaxMinInterval.y));
        }
        

        return tRandomRotation;
    }

    protected virtual void ShouldStart()
    {
        if (mMovingState == State.Start && mNextTimeToStart < Time.time)
        {            
            StartMoving();
        }
    }

    protected virtual void StartMoving()
    {
        mMovingState = State.Stop;
        CalculateStopTime();
        mDirection = CalculateRandomDirection();
    }

    protected virtual void ShouldStop()
    {
        if ((mMovingState == State.Stop && mNextTimeToStop < Time.time) || GetComponent<AILoveBehaviour>().cIsInLove)
        {            
            Stop();
        }
    }

    protected virtual void Stop()
    {
        mMovingState = State.Start;
        CalculateStartTime();
        mDirection = Vector2.zero;
    }


    protected virtual void Move()
    {
        if ((mMovingState == State.Stop && mNextTimeToStart < Time.time) || GetComponent<AILoveBehaviour>().cIsInLove)
        {
            if (Mathf.Abs(HighestSpeed()) > 0.5f && Mathf.Abs(HighestSpeed()) < 2)
            {
                mAnimator.SetFloat("Speed", Mathf.Abs(HighestSpeed()));
            }
            GetComponent<Rigidbody2D>().velocity = mDirection;
            SetFacingRightWay();
        }
        else
        {
            mAnimator.SetFloat("Speed", 0);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        
           
      

    }

    protected virtual float HighestSpeed()
    {
        float tHighestSpeed = float.MinValue;

        if (GetComponent<Rigidbody2D>().velocity.x > GetComponent<Rigidbody2D>().velocity.y)
        {
            tHighestSpeed = GetComponent<Rigidbody2D>().velocity.x;
        }
        if (GetComponent<Rigidbody2D>().velocity.x < GetComponent<Rigidbody2D>().velocity.y)
        {
            tHighestSpeed = GetComponent<Rigidbody2D>().velocity.y;
        }

        return tHighestSpeed;
    }

    protected virtual void SetFacingRightWay()
    {
        if (mDirection.x > 0 && transform.localScale.x > 0)
        {
        Vector3 tLocalScale = transform.localScale;
        tLocalScale.x *= -1;
        transform.localScale = tLocalScale;
        }

        else if (mDirection.x < 0 && transform.localScale.x < 0)
        {
            Vector3 tLocalScale = transform.localScale;
            tLocalScale.x  *= -1;
            transform.localScale = tLocalScale;

        }


    }

    protected virtual void OnTriggerExit2D(Collider2D pCollider)
    {
        if (pCollider.tag == "AIWalkPlace")
        {
            ChangePathWithRaycasts();
        }
       

    }

 

    protected virtual void OnCollisionStay2D(Collision2D pCollision)
    {
        if (pCollision.collider.tag == "AI")
        {
            ChangePathWithRaycasts();
        }
        if (pCollision.collider.tag == "Blocked")
        {
            ChangePathWithRaycasts();
        }
    }
    protected virtual void OnCollisionExit2D(Collision2D pCollision)
    {
        if (pCollision.collider.tag == "Blocked")
        {
        }

    }



    protected virtual void RayCastFrontOfPlayer()
    {
        RaycastHit2D[] tHitUps = Physics2D.RaycastAll(transform.position + new Vector3(0, 0.5f, 0), mDirection, CheckForNpcsDistance);

        RaycastHit2D[] tHits = Physics2D.RaycastAll(transform.position, mDirection, CheckForNpcsDistance);

        RaycastHit2D[] tHitDowns = Physics2D.RaycastAll(transform.position + new Vector3(0, -0.5f, 0), mDirection, CheckForNpcsDistance);

        foreach (RaycastHit2D tHit in tHits)
        {
            foreach (RaycastHit2D tHitDown in tHitDowns)
            {
                foreach (RaycastHit2D tHitUp in tHitUps)
                {

                    if ((tHit.collider.tag == "AI" && tHit.collider.gameObject != gameObject ||
                        tHitDown.collider.tag == "AI" && tHitDown.collider.gameObject != gameObject ||
                        tHitUp.collider.tag == "AI" && tHitUp.collider.gameObject != gameObject))
                    {
                        ChangePathWithRaycasts();
                    }

                }
            }
        }
    }

    protected virtual void ChangePathWithRaycasts()
    {
        List<Vector2> mListOfNotBlockedDirectionVectors = new List<Vector2>();       

        foreach (Vector2 tDirectionVector in DirectionVectorsToCheckFor)
        {
            Collider2D[] tHits = Physics2D.OverlapCircleAll(transform.position + new Vector3(tDirectionVector.x, tDirectionVector.y, transform.position.z), RadiusCheck);

            bool tHitSomethingBlocked = false;


            foreach (Collider2D tHit in tHits)
            {
                if ((tHit.tag == "AI" || tHit.tag == "Blocked")  && tHit.gameObject != gameObject)
                {
                    tHitSomethingBlocked = true;
                }
            }

            if (!tHitSomethingBlocked)
            {
                mListOfNotBlockedDirectionVectors.Add(tDirectionVector);
            }            
        }

        if (mListOfNotBlockedDirectionVectors.Count > 0)
        {

            Vector2 tVectorToUse = mListOfNotBlockedDirectionVectors[Random.Range(0, mListOfNotBlockedDirectionVectors.Count - 1)];
            mDirection.x = tVectorToUse.x * CalculateRandomDirection().x;
            mDirection.y = tVectorToUse.y * CalculateRandomDirection().y;
        }
        else
        {
            Stop();
        }


    }
















 /*   protected virtual void ChangePath()
    {
        bool TookPath = false;
        

        if (mDirection.x > 0)
        {
            TookPath = true;
            mDirection.x = Random.Range(-SpeedMaxMinInterval.x,-SpeedMaxMinInterval.y);
        }
        else if (mDirection.x < 0)
        {
            TookPath = true;
            mDirection.x = Random.Range(SpeedMaxMinInterval.x, SpeedMaxMinInterval.y);
        }


        if (mDirection.y > 0 )
        {
            mDirection.y = Random.Range(-SpeedMaxMinInterval.x, -SpeedMaxMinInterval.y);
        }
        else if (mDirection.y < 0)
        {
            mDirection.y = Random.Range(SpeedMaxMinInterval.x, SpeedMaxMinInterval.y);
        }
    }

    protected virtual bool TakePath()
    {
        bool tTakeThisPath = true;

        float tRandomNumber = Random.Range(0, 100);

        float tRandomChoosenNumber1 = Random.Range(ChangePathMaxMinInterval.x, ChangePathMaxMinInterval.y);
        float tRandomChoosenNumber2 = Random.Range(ChangePathMaxMinInterval.x, ChangePathMaxMinInterval.y);

        if (tRandomChoosenNumber1 > tRandomChoosenNumber2)
        {
            if (tRandomNumber < tRandomChoosenNumber1 && tRandomNumber > tRandomChoosenNumber2)
            {
                tTakeThisPath = true;
            }
        }
        else if (tRandomChoosenNumber1 < tRandomChoosenNumber2)
        {
            if (tRandomNumber > tRandomChoosenNumber1 && tRandomNumber < tRandomChoosenNumber2)
            {
                tTakeThisPath = true;
            }
        }

        return tTakeThisPath;
    } */
}
