using UnityEngine;

public class SpawningEnemies : MonoBehaviour
{

    public GameObject enemies;
    public Transform spawnPoint;

    public float spawnRate = 5.0f;
    private float nextTimeToSpawn;
    public float spawnCap = 10f;
    public float spawns = 0.0f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        SpawnEnemies();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawns <= spawnCap)
        {
            if (Time.time >= nextTimeToSpawn)
            {
                SpawnEnemies();
                nextTimeToSpawn = Time.time + spawnRate;
            }
        }
        else
        {

        }
        
    }

    public void SpawnEnemies()
    {
        Instantiate(enemies, spawnPoint.position, spawnPoint.rotation );
        spawns++;
    }
}
