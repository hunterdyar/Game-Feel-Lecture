using System;
using System.Collections;
using Peggle.Dino;
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
            if (DinoSettingsManager.Settings.VaryCactusTimes)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }

            Vector3 spawnPos = transform.position;
            spawnPos.y = -2f; // Lock Y to ground
            Instantiate(cactusPrefab, spawnPos, Quaternion.identity);
        }
    }

}
