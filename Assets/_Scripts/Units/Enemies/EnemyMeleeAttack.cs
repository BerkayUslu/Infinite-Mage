using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] EnemySettings _enemySettings;
    private IDamageable _damageable;
    private float lastAttackTime;
    private bool isAttacking;

    private float _attackStartTime;

    private void FixedUpdate()
    {

        if (!isAttacking || (Time.time - lastAttackTime < _enemySettings.enemyAttackInterval) || (Time.time - _attackStartTime < _enemySettings.enemyAttackInterval)) return;
        lastAttackTime = Time.time;
        _damageable.TakeDamage(_enemySettings.enemyDamage);

    }
    public void StartAttack(IDamageable damagable)
    {
        if (!isAttacking)
        {
            _attackStartTime = Time.time;
        }
        _damageable = damagable;
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

}
