using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Peggle.Dino
{
	public class BackgroundItemSpawner : MonoBehaviour
	{
		public GameObject[] prefabs;
		public float rangeMin = 0.5f;
		public float rangeMax = 2f;
		public float yMin = -2;
		public float yMax = -2;
		private void OnEnable()
		{
			StartCoroutine(SpawnCactus());
		}

		public IEnumerator SpawnCactus()
		{
			while (gameObject.activeInHierarchy)
			{
				if (DinoSettingsManager.Settings == null)
				{
					yield return null;
					continue;
				}
				yield return new WaitForSeconds(Random.Range(rangeMin, rangeMax));
				
				Vector3 spawnPos = transform.position;
				spawnPos.y = Random.Range(yMin,yMax); // Lock Y to ground
				Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPos, Quaternion.identity);
			}
		}
	}
}