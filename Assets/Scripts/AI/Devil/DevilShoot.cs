using UnityEngine;
using System.Collections;

public class DevilShoot : MonoBehaviour
{

    public float AttackSpeed;
    public Transform SpawnPos;
    public float ThrowWaitTime;

    public GameObject Shot;

    private float mNextTimeToShoot;

    private Animator mAnimator;

    private AILoveBehaviour mAILoveBehaviour;
    private DevilRotateArm mDevilRotateArm;
        
    // Use this for initialization
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mDevilRotateArm = GetComponent<DevilRotateArm>();
        mAILoveBehaviour = mDevilRotateArm.Devil.GetComponent<AILoveBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        ShouldShoot();
    }



    void ShouldShoot()
    {
        if (mNextTimeToShoot < Time.time && !mAILoveBehaviour.cIsInLove)
        {
            CalculateNextTimeToShoot();
            Shoot();
        }

    }

    void Shoot()
    {
        StartCoroutine(Throw());

        GetComponent<AudioSource>().Play();

        Quaternion mShotRotation = transform.rotation;
        mShotRotation.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        Vector2 tSpawnPos = SpawnPos.position;

        GameObject tShotObject = Instantiate(Shot, tSpawnPos, mShotRotation) as GameObject;
        Shot tShot = tShotObject.GetComponent<Shot>();
    }

    IEnumerator Throw()
    {
        mAnimator.SetTrigger("Throw");
        yield return new WaitForSeconds(ThrowWaitTime);
        mAnimator.SetTrigger("Throw");
    }


    void CalculateNextTimeToShoot()
    {
        mNextTimeToShoot = Time.time + AttackSpeed;
    }
}