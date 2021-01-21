using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0 ;
    public float tileLength = 30;
    public int numberOfTiles = 5;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(Random.Range(0,tilePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTile(int tileIndex)
    {
        Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        zSpawn += tileLength;
    }
}
