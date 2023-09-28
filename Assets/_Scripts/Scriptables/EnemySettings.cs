using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/Enemy/Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    public GameObject enemyPrefab;
    public string enemyName;
    public float enemyAttackInterval;
    public int enemyVisionRadius;
    public float enemyAttackRadius;
    public int enemyHealth;
    public int enemyDamage;
    public float enemySpeed;
    public int experience;
    public int damageIncreaseWithLevel;
    public int healthIncreaseWithLevel;
}
