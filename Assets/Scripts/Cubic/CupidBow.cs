using UnityEngine;
using System.Collections;


public class CupidBow : MonoBehaviour {

    public float RotateSpeed;

    private CubicMovement mCubicMovement;


	// Use this for initialization
	void Start () {
        mCubicMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CubicMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
	}

    void Rotate()
    {
        float tAngle = CalculateAngle();

        Quaternion mRotation = Quaternion.identity;

        if (mCubicMovement.cDirectionFacing == Direction.Left)
        {
            mRotation = Quaternion.Euler(0, 0, tAngle + 180);
        }
        else if (mCubicMovement.cDirectionFacing == Direction.Right)
        {
            mRotation = Quaternion.Euler(0, 0, tAngle);
        }
       

        transform.rotation = Quaternion.Slerp(transform.rotation, mRotation, Time.deltaTime * RotateSpeed);
    }

    float CalculateAngle()
    {
        float tAngle = 0;

        Vector2 tMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       
            tAngle = (Mathf.Atan2(tMousePosition.y - transform.position.y, tMousePosition.x - transform.position.x) * Mathf.Rad2Deg);
    
     

        return tAngle;
    }

    

}
