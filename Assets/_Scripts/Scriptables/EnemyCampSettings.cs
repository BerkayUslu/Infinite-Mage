using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCampSettings", menuName = "ScriptableObjects/Enemy/Enemy Camp Settings")]
public class EnemyCampSettings : ScriptableObject
{
    public EnemySettings enemySettings; 
    public GameObject campPrefab;
    public int campVisionRadius;
    public float enemySpawnInterval;
    public int numberOfEnemiesSpawnedPerInterval;

}
