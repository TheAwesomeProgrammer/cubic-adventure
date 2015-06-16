using UnityEngine;
using System.Collections;

public class CubicMovement : MonoBehaviour
{
    public float Force;
    public float MaxSpeed;

    public bool cCanMove;

    public Direction cDirectionFacing { get; set; }

    private float mLastHorizontalMovement;

 //   public float MaxSpeed;
 //   public float AccerationSpeed;

    private Vector2 mDirection;

    private CupidBowSetPositionToPlayer mCupidBowSetPositionToPlayer;




	// Use this for initialization
	void Start () {
	cDirectionFacing = Direction.Right;
    mCupidBowSetPositionToPlayer = GameObject.Find("Bow").GetComponent<CupidBowSetPositionToPlayer>();
    cCanMove = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (cCanMove)
        {
            Move();
        }
       
	}

    void Update()
    {
        CheckForInput();
    }

    void CheckForInput()
    {
        Vector2 tMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (cDirectionFacing == Direction.Right && tMousePos.x < transform.position.x)
        {
            Flip();
        }
        else if (cDirectionFacing == Direction.Left && tMousePos.x > transform.position.x)
        {
            Flip();
        }
    }

    void Move()
    {

        float tHorizontalMovement = Input.GetAxis("Horizontal");
        float tVerticalMovement = Input.GetAxis("Vertical");

  
        if (tHorizontalMovement < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force, 0));
        }
        if (tVerticalMovement < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -Force));
        }

        if (tHorizontalMovement > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Force, 0));
        }
        if (tVerticalMovement > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Force));
        }

        LimitRigidbodySpeed();

        mLastHorizontalMovement = tHorizontalMovement;
    }

    void LimitRigidbodySpeed()
    {
        Vector2 tVelocity = GetComponent<Rigidbody2D>().velocity;
        float tMagnitude = tVelocity.magnitude;
        if (tMagnitude > MaxSpeed)
        {
            tVelocity *= (MaxSpeed / tMagnitude);
            GetComponent<Rigidbody2D>().velocity = tVelocity;
        }
    }

    void Flip()
    {
        mCupidBowSetPositionToPlayer.SetRotation(new Vector3(0, 0, 0));

        if(cDirectionFacing == Direction.Right)
        {
            cDirectionFacing = Direction.Left;
            mCupidBowSetPositionToPlayer.Setoffset(Direction.Left);
        }
        else if(cDirectionFacing == Direction.Left)
        {
            cDirectionFacing = Direction.Right;
            mCupidBowSetPositionToPlayer.Setoffset(Direction.Right);
        }

        Vector3 tLocalScale = transform.localScale;
        tLocalScale.x *= -1;
        transform.localScale = tLocalScale;
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    /*  rigidbody2D.velocity = Vector2.zero;

        float tHorizontalMovement = Input.GetAxis("Horizontal");
        float tVerticalMovement =   Input.GetAxis("Vertical");

        if (tHorizontalMovement < 0)
        {
            mDirection.x = Mathf.Lerp(0, -MaxSpeed, Time.deltaTime * -AccerationSpeed * tHorizontalMovement);
        }
        if (tVerticalMovement < 0)
        {
            mDirection.y = Mathf.Lerp(0, -MaxSpeed, Time.deltaTime * -AccerationSpeed * tVerticalMovement);
        }

        if (tHorizontalMovement > 0)
        {
          mDirection.x = Mathf.Lerp(0, MaxSpeed, Time.deltaTime * AccerationSpeed * tHorizontalMovement);
        }
        if (tVerticalMovement > 0)
        {
           mDirection.y = Mathf.Lerp(0, MaxSpeed, Time.deltaTime * AccerationSpeed * tVerticalMovement);
        }
        

        rigidbody2D.velocity = mDirection;
     */
    
}
