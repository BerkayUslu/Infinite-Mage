using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/Enemy/Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    public GameObject enemyPrefab;
    public int enemyVisionRadius;
    public int enemyHealth;
    public int enemyDamage;
    public float enemySpeed;

}
