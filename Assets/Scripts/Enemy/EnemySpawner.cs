using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab = null;

    [SerializeField]
    private List<GameObject> _enemySpawnPoints = null;

    [SerializeField]
    private int _spawnCount = 12;

    [SerializeField]
    private float _minSpawnDelay = 0.6f, _maxSpawnDelay = 1.2f;

    IEnumerator SpawnEnemyCoroutine()
    {
        while(_spawnCount > 0)
        {
            _spawnCount--;
            var randomIndex = Random.Range(0, _enemySpawnPoints.Count);

            var randomOffset = Random.insideUnitCircle;
            var spawnPoint = _enemySpawnPoints[randomIndex].transform.position + (Vector3)randomOffset;

            SpawnEnemy(spawnPoint);

            var RandomTime = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            yield return new WaitForSeconds(RandomTime);
        }
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
    }

    private void Start()
    {
        if(_enemySpawnPoints.Count > 0)
        {
            foreach(var spawnPoint in _enemySpawnPoints)
            {
                SpawnEnemy(spawnPoint.transform.position);
            }
        }
        StartCoroutine(SpawnEnemyCoroutine());
    }
}
