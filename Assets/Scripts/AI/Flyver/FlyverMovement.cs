using UnityEngine;
using System.Collections;

public class FlyverMovement : AIMovement {

    public float MoveToPlayerRadius;
    public float AttackMoveRadius;
    public float AttackSpeed;

    public int Damage;

    private Transform mPlayer;

    private float mNextTimeToAttack;

    private bool mCanMove;

    private AILoveBehaviour mAILoveBehaviour;

    protected override void Start()
    {
        base.Start();
        mPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        mCanMove = true;
        mAILoveBehaviour = GetComponent<AILoveBehaviour>();
    }

    protected override void Update()
    {
        if (mCanMove)
        {
            base.Update();
        }
        if (!mAILoveBehaviour.cIsInLove)
        {
            IsInMoveToPlayerOrAttackRadius();
        }
        if(mAILoveBehaviour.cIsInLove)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

  

  

    void IsInMoveToPlayerOrAttackRadius()
    {
        float tDistanceToPlayer = Vector2.Distance(mPlayer.position, transform.position);

        if (tDistanceToPlayer < MoveToPlayerRadius && tDistanceToPlayer > AttackMoveRadius && !mAILoveBehaviour.cIsInLove)
        {
            mDirection = (mPlayer.position - transform.position).normalized * Random.Range(SpeedMaxMinInterval.x, SpeedMaxMinInterval.y);
            GetComponent<Rigidbody2D>().velocity = mDirection;
            SetFacingRightWay();
            mCanMove = true;
            StartMoving();
        }
        else if (tDistanceToPlayer < AttackMoveRadius && mNextTimeToAttack < Time.time)
        {
            CalculateNextTimeToAttack();
            Attack();
            mCanMove = false;
        }
    }

    void Attack()
    {
        mPlayer.SendMessage("TakeDamage", Damage);
    }

    void CalculateNextTimeToAttack()
    {
        mNextTimeToAttack = Time.time + AttackSpeed;
    }

}
