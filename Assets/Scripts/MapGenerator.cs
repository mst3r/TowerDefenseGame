using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Tiles Settings")]
    [SerializeField] private int _mapWitdth = 10;
    [SerializeField] private int _mapHight = 10;
    [SerializeField] private float _tileSize =  1;
    [SerializeField] private GameObject _grassTilePrefab;
    [SerializeField] private GameObject _treeTilePrefab;

    [Header("Noise Settings")]
    [SerializeField] private int _noiseSeed = 1276473;
    [SerializeField] private float _noiseFrequency = 100f;
    [SerializeField] private float _noiseThresgold = 0.4f;
    [SerializeField] private float _treesThreshold = 0.7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       MakeMapGrid(); 
    }

    private Vector2 GetHexCoords(int x, int z)
    {
        float xPos = x * _tileSize * Mathf.Cos(Mathf.Deg2Rad * 30);
        float zPos = z * _tileSize + ((x % 2 == 1) ? _tileSize * 0.5f : 0);

        return new Vector2(xPos, zPos);
    }

    void MakeMapGrid()
    {
        for (int x = 0; x < _mapWitdth; x++)
        {
            for (int z = 0; z < _mapHight; z++)
            {

                Vector2 hexCoords = GetHexCoords(x, z);
                Vector3 position = new Vector3(hexCoords.x, 0, hexCoords.y);

                //if the noiseSeed is -1, make random seed;
                if (_noiseSeed == -1) _noiseSeed = Random.Range(0, 10000000);

                //get noise value(0-1)
                float noiseValue = Mathf.PerlinNoise((hexCoords.x + _noiseSeed) / _noiseFrequency, (hexCoords.y + _noiseSeed) / _noiseThresgold);

                //inititate defualt tile as grass
                GameObject prefab = _grassTilePrefab;

                //if the noiseValue is less that the threshold, do not spwwan a tile, just contirnue
                if (noiseValue < _noiseThresgold) continue;

                //if the noiseValue is above the Threshold, make the the trees instead
                if (noiseValue > _treesThreshold) prefab = _treeTilePrefab;

                //only instantiate the tile if its not water
                GameObject tile = Instantiate(prefab, position, Quaternion.Euler(90, 0, 0));
            }

        }
        PlayerControlls playerController = FindObjectOfType<PlayerControlls>();
        if (playerController != null)
        {
            playerController.minBounds = Vector2.zero;
            playerController.maxBounds = new Vector2(_mapWitdth, _mapHight);
        }
    }
}
