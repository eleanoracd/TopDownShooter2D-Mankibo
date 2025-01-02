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
    private float waveEndTime;

    private void Start()
    {
        waveEndTime = Time.time + waveDuration;
        StartCoroutine(WaveSpawner());
    }

    private IEnumerator WaveSpawner()
    {
        while (Time.time < waveEndTime)
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
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("No Spawn points assigned!");
            return;
        }

        
        spawnPoints.RemoveAll(spawnPoint => spawnPoint == null);

        if (spawnPoints.Count == 0)
        {
            Debug.LogError("All spawn points have been destroyed!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[randomIndex];

        if (spawnPoint != null)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Selected spawn point is null. Skipping spawn.");
        }
    }

    public bool IsWaveOver()
    {
        return Time.time >= waveEndTime;
    }

    public float GetWaveTimeRemaining()
    {
        return Mathf.Max(0, waveEndTime - Time.time);
    }

    private void OnDestroy()
    {
        
        StopAllCoroutines();
    }
}