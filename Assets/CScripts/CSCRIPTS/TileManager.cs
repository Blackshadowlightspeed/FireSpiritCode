using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    public Transform playerTransform;
    public float spawnZ = 0.0f;
    public float tileLength = 10.0f;
    public int numberOfTiles = 5;

    private List<GameObject> activeTiles = new List<GameObject>();


    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 15 > spawnZ - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTiles();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnZ, transform.rotation);
        activeTiles.Add(go);
        spawnZ += tileLength;
    }

    private void DeleteTiles()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}


