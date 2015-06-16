using UnityEngine;
using System.Collections;

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class CupidShot : MonoBehaviour {

    public float AttackSpeed;

    public GameObject Shot;

    private float mNextTimeToShoot;

    private CubicMovement mCubicMovement;

    private Animator mAnimator;

    


	// Use this for initialization
	void Start () {
        mCubicMovement =  GameObject.FindGameObjectWithTag("Player").GetComponent<CubicMovement>();
        mAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        ShouldShoot();
	}



    void ShouldShoot()
    {
        if (Input.GetButtonUp("Shoot") && mNextTimeToShoot < Time.time)
        {
            CalculateNextTimeToShoot();
            Shoot();
            mAnimator.SetTrigger("ExpandNShrink");
        }
        else if (Input.GetButtonDown("Shoot"))
        {
            mAnimator.SetTrigger("ExpandNShrink");
        }

    }

    void OnTriggerEnter2D(Collider2D pCollider)
    {
        if (pCollider.tag == "Bottom")
        {
            GetComponent<AudioSource>().Play();
        }
    }

    void Shoot()
    {
        Quaternion mShotRotation = transform.rotation;
        mShotRotation.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        GetComponent<AudioSource>().Play();
        GameObject tShotObject = Instantiate(Shot, transform.position, mShotRotation) as GameObject;
        Shot tShot = tShotObject.GetComponent<Shot>();
        tShot.cShootDirection = mCubicMovement.cDirectionFacing;
    }

    void CalculateNextTimeToShoot()
    {
        mNextTimeToShoot = Time.time + AttackSpeed;
    }
}
