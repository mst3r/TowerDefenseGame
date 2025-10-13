using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawningEnemies : MonoBehaviour
{

    public GameManager GameManager;

    public GameObject enemies;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject manager;

    public Transform spawnPoint;

    public float spawnRate = 5.0f;
    private float nextTimeToSpawn;
    public float spawnCap;
    public float spawns;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        manager = GameObject.FindWithTag("Manager");
        GameManager = manager.GetComponent<GameManager>();

        spawnCap = GameManager.spawnCap;
        spawns = GameManager.spawns;

        SpawnEnemies();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawns < spawnCap)
        {
            if (Time.time >= nextTimeToSpawn)
            {
                SpawnEnemies();
                nextTimeToSpawn = Time.time + spawnRate;

                GameManager.UpdateSpawncap();
                spawns = GameManager.spawns;

                //Debug.Log(spawns);

            }
        }
        else
        {

        }
        
    }

    public void SpawnEnemies()
    {
        if (GameManager.playerLevel == 0)
        {
            float enemyToSpawn = Random.Range(0.0f, 10.0f);

            if (enemyToSpawn <= 1)
            {
                spawns++;
                Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation);

            }
            else if (enemyToSpawn >= 2)
            {
                spawns++;
                Instantiate(enemies, spawnPoint.position, spawnPoint.rotation);
            }
        }
        // Easiest level [Basic enemies spawn and 10% change for fast 

        if (GameManager.playerLevel == 5)
        {
            float enemyToSpawn = Random.Range(0.0f, 10.0f);

            if (enemyToSpawn <= 3)
            {
                spawns++;
                Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation);

            }
            else if (enemyToSpawn >= 6)
            {
                spawns++;
                Instantiate(enemies, spawnPoint.position, spawnPoint.rotation);
            }
            else if (enemyToSpawn > 3 && enemyToSpawn < 6)
            {
                spawns++;
                Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
            }
            
        }
        //Basic level [Basic enemies 60%, fast enemies 30%, tanks 10%

        if (GameManager.playerLevel == 15)
        {
            float enemyToSpawn = Random.Range(0.0f, 10.0f);

            if (enemyToSpawn < 5)
            {
                spawns++;
                Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation);

            }
            else if (enemyToSpawn > 8 )
            {
                spawns++;
                Instantiate(enemies, spawnPoint.position, spawnPoint.rotation);
            }
            else if (enemyToSpawn > 5 && enemyToSpawn <= 8)
            {
                spawns++;
                Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
            }

        }
        //Medium Level [Basic enemies 20%, fast enemies 50%, tanks 30%

        if (GameManager.playerLevel == 30)
        {
            float enemyToSpawn = Random.Range(0.0f, 10.0f);

            if (enemyToSpawn <= 3)
            {
                spawns++;
                Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation);

            }
            else if (enemyToSpawn > 9)
            {
                spawns++;
                Instantiate(enemies, spawnPoint.position, spawnPoint.rotation);
            }
            else if (enemyToSpawn > 3 && enemyToSpawn <= 9)
            {
                spawns++;
                Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
            }

        }
        //Hard Level [Basic enemies 10%, fast enemies 30%, tanks 60%





    }
}
