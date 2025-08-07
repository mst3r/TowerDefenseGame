using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int _mapWitdth = 10;
    [SerializeField] private int _mapHight = 10;
    [SerializeField] private int _tileSize =  1;
    [SerializeField] private GameObject _tilePrefab;

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
                GameObject tile = Instantiate(_tilePrefab, position, Quaternion.Euler(90, 0, 0));
            }
    
        }
    }
}
