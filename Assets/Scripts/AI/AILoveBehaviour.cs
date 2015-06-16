using UnityEngine;
using System.Collections;

public class AILoveBehaviour : MonoBehaviour {

    public GameObject HeartSystem;
    public GameObject Puff;

    public bool cIsInLove { get; set; }

    private AILoveFindEachOther mAILoveFindEachOther;
    private AIMovement mAIMovement;

    private GameObject mOtherPersonInLove;

    private bool mHasStartedMovingToLove;
    private bool mHasSpawnedHeartSystem;

    private ComboManager mComboManager;
    private ComboWriter mComboWriter;

	// Use this for initialization
	void Start () {
        mAILoveFindEachOther = GetComponent<AILoveFindEachOther>();
        mAIMovement = GetComponent<AIMovement>();
        mComboManager = GameObject.Find("ComboManager").GetComponent<ComboManager>();
        mComboWriter = GameObject.Find("ComboWriter").GetComponent<ComboWriter>();
	}
	
	// Update is called once per frame
	void Update () {
        ShouldMoveToLove();
	}

    void ShouldMoveToLove()
    {
        mOtherPersonInLove = mAILoveFindEachOther.FindOtherPersonInLove();

        if (mOtherPersonInLove && !mHasStartedMovingToLove && cIsInLove)
        {
            MoveToLove();
        }
    }

    void MoveToLove()
    {
        mAIMovement.FollowATargetAndCallBack(gameObject, mOtherPersonInLove);
        mHasStartedMovingToLove = true;
    }

    void Callback(GameObject pOnePersonInLove)
    {
      Vector3 tWantedSpawnPos =  pOnePersonInLove.transform.position + new Vector3 (0,0,-5);
      GetComboPointAndName(pOnePersonInLove);
      Instantiate(Puff, tWantedSpawnPos, Quaternion.identity);
            if (pOnePersonInLove.transform.parent != null)
            {
                Destroy(pOnePersonInLove.transform.parent.gameObject);
                SpawnManager.EnemiesDied++;
            }
            else
            {
                Destroy(pOnePersonInLove);
                SpawnManager.EnemiesDied++;
            }
            if (mOtherPersonInLove.transform.parent != null)
            {
                Destroy(mOtherPersonInLove.transform.parent.gameObject);
                SpawnManager.EnemiesDied++;
            }
            else
            {
                Destroy(mOtherPersonInLove.gameObject);
                SpawnManager.EnemiesDied++;
            }
        
       
        mHasStartedMovingToLove = false;

    }

    void GetComboPointAndName(GameObject pOnePersonInLove)
    {
        int tComboPoints = mComboManager.FindBestComboAndReturnPoints(pOnePersonInLove.GetComponent<ComboId>().ComboIds, mOtherPersonInLove.GetComponent<ComboId>().ComboIds);
        string tNameOfCombo = mComboManager.cCurrentComboName;

        ScoreManager.sScore += tComboPoints;
        mComboWriter.AddComboName(tNameOfCombo);
    }

    void OnTriggerEnter2D(Collider2D pCollider)
    {
        if (pCollider.tag == "CupidShot")
        {           
            if (!mHasSpawnedHeartSystem)
            {
                mHasSpawnedHeartSystem = true;
                InLove();
            }
            GetComponent<AudioSource>().Play();

                Destroy(pCollider.gameObject);
        }
    }

    void InLove()
    {
        HeartParticlesFollow tHeartSystemFollow = SpawnHeartSystem().GetComponent<HeartParticlesFollow>();
        tHeartSystemFollow.SetTarget(gameObject);

        cIsInLove = true;
    }

    GameObject SpawnHeartSystem()
    {
        GameObject tHeartSystem = Instantiate(HeartSystem, transform.position, Quaternion.identity) as GameObject;
        return tHeartSystem;
    }
}
