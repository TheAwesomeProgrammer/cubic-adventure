using UnityEngine;
using System.Collections;

public class DevilMovement : AIMovement {

    public GameObject mArm;

    public Direction cMovingDirection;

    private Transform mPlayer;    

    protected override void Start()
    {
        base.Start();
        mPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        
    }


	protected override void SetFacingRightWay()
    {
        Vector2 tPlayerPos = mPlayer.position;
        SetPositionToDevil tSetPosToDevil = mArm.GetComponent<SetPositionToDevil>();

        if (tPlayerPos.x > transform.position.x && transform.localScale.x > 0)
        {
            Flip();
            tSetPosToDevil.Setoffset(Direction.Left);
            cMovingDirection = Direction.Left;
        }
        else if (tPlayerPos.x < transform.position.x && transform.localScale.x < 0)
        {
            Flip();
            tSetPosToDevil.Setoffset(Direction.Right);
            cMovingDirection = Direction.Right;
        }
    }

    void Flip()
    {
        
        
        Vector3 tLocalScale = transform.localScale;
        tLocalScale.x *= -1;
        transform.localScale = tLocalScale;
    }
}
