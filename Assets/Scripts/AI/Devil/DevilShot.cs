using UnityEngine;
using System.Collections;

public class DevilShot : MonoBehaviour {

    public int Damage;
    public float Force;

	// Use this for initialization
	void Start () {
        Move();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Move()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.up) * Force);
    }

    void OnTriggerEnter2D(Collider2D pCollider)
    {
        if (pCollider.tag == "Player")
        {
            pCollider.SendMessage("TakeDamage", Damage);
            Destroy(gameObject);
        }
    }
}
