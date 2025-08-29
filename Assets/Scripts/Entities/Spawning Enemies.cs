using UnityEngine;

public class SpawningEnemies : MonoBehaviour
{

    public GameObject enemies;
    public Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        SpawnEnemies();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        Instantiate(enemies, spawnPoint.position, spawnPoint.rotation );
    }
}
