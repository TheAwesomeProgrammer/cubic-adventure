using UnityEngine;
using System.Collections;

public class AILoveFindEachOther : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject FindOtherPersonInLove()
    {
        GameObject tOtherPersonInLove = null;

        float tSmallestDistanceToPerson = float.MaxValue;

        foreach (GameObject tOtherPerson in GameObject.FindGameObjectsWithTag("AI"))
        {
            if (tOtherPerson.gameObject != gameObject)
            {
                AILoveBehaviour tAILoveBehaviour = tOtherPerson.GetComponent<AILoveBehaviour>();
                if (tAILoveBehaviour != null)
                {
                    float tDistanceToPerson = Vector2.Distance(transform.position, tOtherPerson.transform.position);

                    if (tAILoveBehaviour.cIsInLove && tDistanceToPerson < tSmallestDistanceToPerson)
                    {
                        tOtherPersonInLove = tOtherPerson;
                        tSmallestDistanceToPerson = tDistanceToPerson;
                    }
                }
            }
        }

        return tOtherPersonInLove;
    }
}
