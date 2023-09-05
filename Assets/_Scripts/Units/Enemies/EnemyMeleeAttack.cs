using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] EnemySettings _enemySettings;
    private IDamageable _damageable;
    private float attackTime = 1;
    private float lastAttackTime;
    private bool isItAttacking;

    private void FixedUpdate()
    {
        if (!isItAttacking || (Time.time-lastAttackTime < attackTime)) return;
        lastAttackTime = Time.time;
        _damageable.TakeDamage(_enemySettings.enemyDamage);

    }
    public void StartAttack(IDamageable damagable)
    {
        _damageable = damagable;
        isItAttacking = true;
    }

    public void StopAttack()
    {
        isItAttacking = false;
    }
}
