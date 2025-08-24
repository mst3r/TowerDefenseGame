using UnityEngine;

public class EnemySpawners : MonoBehaviour
{
    private float mapHeight;
    private float mapWidth;

    private float sideOne;
    private float sideTwo;
    private float sideThree;
    private float sideFour;

    public Transform Transform;

    //public MapGenerator MapGenerator;

    public GameObject EnemySpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mapHeight = 50f;
        mapWidth = 60f;

        sideOne = Random.Range(0f, mapHeight);
        sideTwo = Random.Range(0f, mapWidth);
        sideThree = Random.Range(0f, mapHeight);
        sideFour = Random.Range(0f, mapWidth);

        Vector3 SpawnPos1 = new Vector3(0f, 1f, sideOne);
        Vector3 SpawnPos2 = new Vector3(sideTwo, 1f, 0f);
        Vector3 SpawnPos3 = new Vector3(mapHeight, 1f, sideThree);
        Vector3 SpawnPos4 = new Vector3(sideFour, 1f, mapWidth);

        /*
        Instantiate(EnemySpawner, SpawnPos1, Quaternion.identity);
        Instantiate(EnemySpawner, SpawnPos2, Quaternion.identity);
        Instantiate(EnemySpawner, SpawnPos3, Quaternion.identity);
        Instantiate(EnemySpawner, SpawnPos4, Quaternion.identity);
        */

        SpawnTowers(SpawnPos1, SpawnPos2, SpawnPos3, SpawnPos4);




    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTowers(Vector3 SpawnPos1, Vector3 SpawnPos2, Vector3 SpawnPos3, Vector3 SpawnPos4)
    {
        Vector3[] spawnPositions = { SpawnPos1, SpawnPos2, SpawnPos3, SpawnPos4 }; 

        foreach (Vector3 pos in spawnPositions)
        {
            GameObject EnemyTower = Instantiate(EnemySpawner, pos, Quaternion.identity);

            EnemyTower.transform.LookAt(Transform);
        }
    }
}
