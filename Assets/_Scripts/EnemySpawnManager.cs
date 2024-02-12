using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _enemiesTransform;
    [SerializeField] PlayerLevelAndStats _playerExperienceClass;
    [SerializeField] GameObject _enemyPrefab;

    [SerializeField] float _spawningInnerRadius = 30f;
    [SerializeField] float _spawningOutterRadius = 50f;

    [SerializeField] int _initialSpawnAmount = 5;
    [SerializeField] int _spawnAmountModifier = 0;
    [SerializeField] int _spawnAmountModificationTriggerTime = 10;

    [SerializeField] int _experiencePointsModification = 0;

    [SerializeField] int _spawnHealthModification = 0;
    [SerializeField] float _healthModificationInterval = 60;
    [SerializeField] float _lastHealthModificationTime = 0;

    [SerializeField] float _spawningInterval = 5;
    [SerializeField] float _lastSpawnTime = 0;

    [SerializeField] List<GameObject> _enemyPool;

    private void Start()
    {
        SpawnEnemy(_initialSpawnAmount);
    }

    private void Update()
    {
        if(Time.time - _lastSpawnTime > _spawningInterval)
        {
            SpawnEnemy(_initialSpawnAmount + _spawnAmountModifier);
        }

        if(Time.time > _spawnAmountModificationTriggerTime)
        {
            if(_spawnAmountModificationTriggerTime < 741)
            {
                _spawnAmountModificationTriggerTime = _spawnAmountModificationTriggerTime * 2;
            }
            else
            {
                _spawnAmountModificationTriggerTime += 120;
            }

            _spawnAmountModifier += (int)(0.5f * (_initialSpawnAmount + _spawnAmountModifier));
        }

        if (Time.time - _lastHealthModificationTime > _healthModificationInterval)
        {
            _lastHealthModificationTime = Time.time;

            _spawnHealthModification += 6;
            _experiencePointsModification += 25;
        }
    }

    private void SpawnEnemy(int spawnAmount)
    {
        _lastSpawnTime = Time.time;

        for(int i = 0; i < spawnAmount; i++)
        {
            float spawningPointDistanceFromPlayer = Random.Range(_spawningInnerRadius, _spawningOutterRadius);
            float spawningDegree = Random.Range(0, 360);
            float xOfDistanceFromPlayer = Mathf.Sin(Mathf.Deg2Rad * spawningDegree) * spawningPointDistanceFromPlayer;
            float zOfDistanceFromPlayer = Mathf.Cos(Mathf.Deg2Rad * spawningDegree) * spawningPointDistanceFromPlayer;
            Vector3 spawningPosition = Vector3.Scale(_playerTransform.position, Vector3.right + Vector3.forward) + new Vector3(xOfDistanceFromPlayer, 3.5f, zOfDistanceFromPlayer);

            GameObject tempEnemy = SearchForInactiveEnemy();

            if(tempEnemy == null)
            {
                tempEnemy = Instantiate(_enemyPrefab, spawningPosition, Quaternion.identity, _enemiesTransform);
                _enemyPool.Add(tempEnemy);
            }
            else
            {
                tempEnemy.SetActive(true);
            }

            tempEnemy.transform.position = spawningPosition;

            EnemyMovement tempEnemyMovement = tempEnemy.GetComponent<EnemyMovement>();
            tempEnemyMovement.SetPlayerTransform(_playerTransform);
            AlienFishEnemy tempEnemyFish = tempEnemy.GetComponent<AlienFishEnemy>();
            tempEnemyFish.SetHealthModification(_spawnHealthModification);
            tempEnemyFish.SetPlayerExperienceClass(_playerExperienceClass);
            tempEnemyFish.ModifyExperiencePoints(_experiencePointsModification);
        }
    }

    private GameObject SearchForInactiveEnemy()
    {
        foreach (GameObject item in _enemyPool)
        {
            if (!item.activeInHierarchy)
            {
                return item;
            }
        }
        return null;
    }
}
