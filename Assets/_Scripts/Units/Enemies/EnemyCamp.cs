using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamp : MonoBehaviour
{
    //champion
    //spawn, while spawning share the level
    //vision share

    private EnemyCampSettings _enemyCampSettingSO;
    private Transform _transform;
    private Transform _playerTransform;
    public Action<Vector3> CampSpottedPlayer;
    private EnemySpawnPool _enemySpawnPool;
    private bool playerInVision = false;
    private float _nextSpawnTime;
    private Coroutine campWatchCoroutine;

    private Vector3 _spawnDistanceVector = new Vector3(1, 0, 1);

    private void OnEnable()
    {
        if (_enemyCampSettingSO == null) return;
        campWatchCoroutine = StartCoroutine("CheckPlayerDistanceAndStartSpawningCoroutine");
    }

    private void OnDisable()
    {
        StopCoroutine(campWatchCoroutine);
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _enemySpawnPool = FindAnyObjectByType<EnemySpawnPool>();
        if (campWatchCoroutine != null) return;
        campWatchCoroutine = StartCoroutine("CheckPlayerDistanceAndStartSpawningCoroutine");
    }

    private void Update()
    {
        if (!playerInVision || Time.time < _nextSpawnTime) return;
        SpawnMobs();
    }

    private void SpawnMobs()
    {
        _nextSpawnTime = Time.time + _enemyCampSettingSO.enemySpawnInterval;
        for(int i = 0; i < _enemyCampSettingSO.numberOfEnemiesSpawnedPerInterval ; i++)
        {
            GameObject spawn = _enemySpawnPool.GetPooledObjectOrCreateIfNotAvailable(_enemyCampSettingSO.enemySettings.enemyPrefab, _enemyCampSettingSO.enemySettings.enemyName);
            spawn.transform.position = _transform.position + UnityEngine.Random.Range(-1f, 1f) * _spawnDistanceVector;
            spawn.SetActive(true);
        }
    }

    private IEnumerator CheckPlayerDistanceAndStartSpawningCoroutine()
    {
        while (true)
        {
            float distanceFromPlayer = (_transform.position - _playerTransform.position).magnitude;
            if(distanceFromPlayer < _enemyCampSettingSO.campVisionRadius)
            {
                CampSpottedPlayer?.Invoke(_playerTransform.position);
                playerInVision = true;
            }
            else
            {
                playerInVision = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetVariables(EnemyCampSettings set, Transform playerTransform)
    {
        _enemyCampSettingSO = set;
        _playerTransform = playerTransform;
    }
}
