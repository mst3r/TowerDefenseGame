using UnityEngine;

public class HomeBaseSpawner : MonoBehaviour
{
    private float mapHeight = 50f;
    private float mapWidth = 60f;

    public float spawnX;
    public float spawnY;

    public GameObject MainBase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnX = mapHeight / 2;
        spawnY = mapWidth / 2;

        Vector3 spawnPos = new Vector3(spawnX, 1f, spawnY);

        Instantiate(MainBase, spawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
