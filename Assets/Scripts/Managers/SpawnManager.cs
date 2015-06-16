using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

    public static int EnemiesDied;

    public GameObject[] ObjectsToSpawn;
    public GameObject[] Enemies;

    public int StartingAmountOfDangerEnemiesPerRound;
    public int StartingAmountOfEnemies;
    public int StartingGlobalIncreaseAmountEnemiesDied;
    public float StartingIncreasePerGlobalIncrease;
    public float IntervalToSpawnAt;
    public int StartingEnemiesToSpawnPerRound;

    private int AmountOfDangerEnemiesPerRound;
    private int StartAmountOfEnemies;
    private float GlobalIncreaseWhenAmountOfEnemiesDied;
    private float IncreasePerGlobalIncrease;
    private int EnemiesToSpawnPerRound;

    private float GlobalIncrease;

    public Transform GroundToSpawnAt;

    private float mNextTimeToSpawn;

    private int mEnemiesDiedToNextTimeGlobalIncrease;
    

	// Use this for initialization
	void Start () {
        SpawnEnemies(StartingAmountOfEnemies);
        GlobalIncrease = 1;
        CalculateNextTimeToSpawn();
        mEnemiesDiedToNextTimeGlobalIncrease = (int)StartingGlobalIncreaseAmountEnemiesDied;
	}

    void MakeEveythingMulitipliedByGlobalIncrease()
    {
        AmountOfDangerEnemiesPerRound = (int)(StartingAmountOfDangerEnemiesPerRound * GlobalIncrease);
        StartAmountOfEnemies = (int)(StartingAmountOfEnemies * GlobalIncrease);
        GlobalIncreaseWhenAmountOfEnemiesDied = StartingGlobalIncreaseAmountEnemiesDied * GlobalIncrease;
        IncreasePerGlobalIncrease = StartingIncreasePerGlobalIncrease * GlobalIncrease;
        EnemiesToSpawnPerRound = (int)(StartingEnemiesToSpawnPerRound * GlobalIncrease);
    }
	
	// Update is called once per frame
	void Update () {
        ShouldSpawn();
        MakeEveythingMulitipliedByGlobalIncrease();
        ShouldIncreaseGlobalincrease();
	}

    void ShouldIncreaseGlobalincrease()
    {
        if (mEnemiesDiedToNextTimeGlobalIncrease <= EnemiesDied)
        {
            mEnemiesDiedToNextTimeGlobalIncrease += (int)GlobalIncreaseWhenAmountOfEnemiesDied;
            GlobalIncrease += IncreasePerGlobalIncrease;
        }
    }


    void ShouldSpawn()
    {
        if (mNextTimeToSpawn < Time.time)
        {
            CalculateNextTimeToSpawn();
            SpawnRound();
        }
    }

    void SpawnRound()
    {
        int tEnemiesToSpawn = EnemiesToSpawnPerRound * (1 - (EnemiesAlive() / StartingAmountOfEnemies));
        SpawnEnemies(tEnemiesToSpawn);
        int tDangerEnemiesToSpawn = AmountOfDangerEnemiesPerRound * (1 - (EnemiesAlive() / StartingAmountOfEnemies));
        SpawnDangerEnemies(tEnemiesToSpawn);
    }



    void CalculateNextTimeToSpawn()
    {
        mNextTimeToSpawn = Time.time + IntervalToSpawnAt;
    }

    void SpawnEnemies(int AmountOfEnemies)
    {      

        for (int i = 0; i < AmountOfEnemies; i++)
        {
            int tRandomNumber = Random.Range(0, ObjectsToSpawn.Length);

            Instantiate(ObjectsToSpawn[tRandomNumber], PlaceToSpawn(), Quaternion.identity);
        }
    }

    void SpawnDangerEnemies(int AmountOfEnemies)
    {

        for (int i = 0; i < AmountOfEnemies; i++)
        {

            int tRandomNumber = Random.Range(0, Enemies.Length);

            GameObject tSpawnObject = Enemies[tRandomNumber];

            Vector2 tPlaceToSpawn = Vector2.zero;

            if (tSpawnObject.name != "Flyver")
            {
              tPlaceToSpawn = PlaceToSpawn();
            }
            else
            { 
               // tPlaceToSpawn = FlyverPlaceToSpawn();
            }

            

            
            Instantiate(tSpawnObject, tPlaceToSpawn, Quaternion.identity);
        }
    }

  


    Vector2 PlaceToSpawn()
    {
        Vector2 tPlaceToSpawn = Vector2.zero;

            Camera tCam = Camera.main;
            float tHeight = 2f * tCam.orthographicSize;
            float tWidth = tHeight * tCam.aspect;

            while ((Mathf.Abs(tPlaceToSpawn.x - Camera.main.transform.position.x) < (tWidth / 2) &&
                   Mathf.Abs(tPlaceToSpawn.y - Camera.main.transform.position.y) < (tHeight / 2)) ||
                   tPlaceToSpawn.y > -1)
            {
                tPlaceToSpawn = GroundToSpawnAt.position;

                float tRandomNumber = Random.Range(0, 1);


                float RandomX = Random.Range(-(GroundToSpawnAt.localScale.x / 2), GroundToSpawnAt.localScale.x / 2);
                float RandomY = Random.Range(-(GroundToSpawnAt.localScale.y / 2), GroundToSpawnAt.localScale.y / 2);


                    tPlaceToSpawn += new Vector2(RandomX, RandomY);   

                
            }

        return tPlaceToSpawn;
    }

    Vector2 FlyverPlaceToSpawn()
    {
        Vector2 tPlaceToSpawn = Vector2.zero;

        Camera tCam = Camera.main;
        float tHeight = 2f * tCam.orthographicSize;
        float tWidth = tHeight * tCam.aspect;

        while ((Mathf.Abs(tPlaceToSpawn.x - Camera.main.transform.position.x) < (tWidth / 2) &&
               Mathf.Abs(tPlaceToSpawn.y - Camera.main.transform.position.y) < (tHeight / 2)) ||
               tPlaceToSpawn.y < 0)
        {
            tPlaceToSpawn = GroundToSpawnAt.position;

            float tRandomNumber = Random.Range(0, 1);


            float RandomX = Random.Range(-(GroundToSpawnAt.localScale.x / 2), GroundToSpawnAt.localScale.x / 2);
            float RandomY = Random.Range(-(GroundToSpawnAt.localScale.y / 2), GroundToSpawnAt.localScale.y / 2);


            tPlaceToSpawn += new Vector2(RandomX, RandomY);


        }

        return tPlaceToSpawn;
    }

    int EnemiesAlive()
    {
        return GameObject.FindGameObjectsWithTag("AI").Length;


    }
}
