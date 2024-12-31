using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float spawnInterval = 2.5f;
    [SerializeField] private float waveDuration = 120f;

    private bool isSpawning = true;

    private void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    private IEnumerator WaveSpawner()
    {
        float endTime = Time.time + waveDuration;

        while (Time.time < endTime)
        {
            if (isSpawning)
            {
                SpawnEnemyAtRandomPoint();
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        isSpawning = false;
        Debug.Log("Wave Ended!");
    }

    private void SpawnEnemyAtRandomPoint()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No Spawn points assigned!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
