using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

    public float Force;


    public Direction cShootDirection { get; set; }

    private GameObject mObjectToLookAt;

	// Use this for initialization
	void Start () {
        Move();
        LookAtGround();
        Torque();
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void LookAtGround()
    {
        if (cShootDirection == Direction.Right)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
        }
        if (cShootDirection == Direction.Left)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 90);
        }
    }

    void Torque()
    {     

        if (cShootDirection == Direction.Right)
        {
            float tTorque = 1 * ((transform.eulerAngles.z - 180) / 180);

            GetComponent<Rigidbody2D>().AddTorque(-tTorque);

        }
        if (cShootDirection == Direction.Left)
        {
            float tTorque = 1f * ((Mathf.Abs(transform.eulerAngles.z - 360) - 180) / 180);


            GetComponent<Rigidbody2D>().AddTorque(tTorque);
        }
    }

    void Move()
    {
        if (cShootDirection == Direction.Right)
        {          

            GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.right) * Force);
            
        }
        if (cShootDirection == Direction.Left)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.left) * Force);   
        }
    }

}
