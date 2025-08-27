using UnityEngine;

public class HomeBaseSpawner : MonoBehaviour
{
    private float mapHeight = 30f;
    private float mapWidth = 35f;

    public float spawnX;
    public float spawnY;

    public GameObject MainBase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnX = mapHeight / 2;
        spawnY = mapWidth / 2;
        //Halfs the width and height of the map to get the center

        Vector3 spawnPos = new Vector3(spawnX, 1f, spawnY);
        //Creates a Vector3 for the spawn position

        Instantiate(MainBase, spawnPos, Quaternion.identity);
        //Spawns the player base
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
