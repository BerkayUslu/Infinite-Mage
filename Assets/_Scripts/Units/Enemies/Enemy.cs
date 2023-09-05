using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] EnemySettings _enemySettingsOS;
    private bool Attacking;
    private int _health;
    private int _damage;
    private EnemyMeleeAttack _enemyMeleeAttack;
    private Transform _transform;

    private void Awake()
    {
        _enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
        _transform = transform;
        _health = _enemySettingsOS.enemyHealth;
        _damage = _enemySettingsOS.enemyDamage;
    }

    private void FixedUpdate()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, 3.5f, LayerMask.GetMask("Player"));
        foreach(Collider collider in collisions)
        {
            if(collider.tag == "Player")
            {
                _enemyMeleeAttack.StartAttack(collider.GetComponent<IDamageable>());
            }
        }
        if(collisions.Length == 0)
        {
            _enemyMeleeAttack.StopAttack();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public bool IsItDead() { return _health <= 0; }
    public bool IsItAttacking() { return Attacking; }
}
