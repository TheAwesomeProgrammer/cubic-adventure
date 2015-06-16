using UnityEngine;
using System.Collections;

public class CupidHealth : MonoBehaviour {

    public GameObject Death;

    public int Health = 3;

    public int cHealth { get; set; }

	// Use this for initialization
	void Start () {
        cHealth = Health;
	}
	
	// Update is called once per frame
	void Update () {
        ShouldDie();
	}

    void ShouldDie()
    {
        if (cHealth <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        Death.SetActive(true);
        GetComponent<CubicMovement>().cCanMove = false;
        foreach(GameObject tAI in GameObject.FindGameObjectsWithTag("AI"))
        {
            if(tAI.transform.parent != null)
            {
                Destroy(tAI.transform.parent.gameObject);

            }
            else
            {
            Destroy(tAI.gameObject);
            }
        }
    }

    void TakeDamage(int pDamage)
    {
        cHealth -= pDamage;
    }
}
