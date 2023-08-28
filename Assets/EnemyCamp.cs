using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamp : MonoBehaviour
{
    private EnemyCampSettings _enemyCampSettingSO;
    private Transform _transform;
    private Transform _playerTransform;
    public Action<Vector3> CampSpottedPlayer;
    private bool playerInVision = false;
    private float _nextSpawnTime;
    private Coroutine campWatchCoroutine;

    private void OnEnable()
    {
        if (_enemyCampSettingSO == null) return;
        campWatchCoroutine = StartCoroutine("CheckPlayerDistanceAndAlertSpawnsCoroutine");
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
        if (campWatchCoroutine != null) return;
        campWatchCoroutine = StartCoroutine("CheckPlayerDistanceAndAlertSpawnsCoroutine");
    }

    private void Update()
    {
        if (!playerInVision || Time.time < _nextSpawnTime) return;
        SpawnMobs();
    }

    private void SpawnMobs()
    {
        _nextSpawnTime = Time.time + _enemyCampSettingSO.enemySpawnInterval;
        for(int i = 0; i < 1; i++)
        {
            Instantiate(_enemyCampSettingSO.enemyPrefabToSpawn, _transform.position, Quaternion.identity);
        }
    }

    private IEnumerator CheckPlayerDistanceAndAlertSpawnsCoroutine()
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
