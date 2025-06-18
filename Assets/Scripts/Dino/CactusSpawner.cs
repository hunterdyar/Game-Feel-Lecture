using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CactusSpawner : MonoBehaviour
{
    public GameObject cactusPrefab;
    public bool isSpawning;
    void Start()
    {
        StartCoroutine(SpawnCactus());
    }

    public IEnumerator SpawnCactus()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            Vector3 spawnPos = transform.position;
            spawnPos.y = -2f; // Lock Y to ground
            Instantiate(cactusPrefab, spawnPos, Quaternion.identity);
        }
    }

}
