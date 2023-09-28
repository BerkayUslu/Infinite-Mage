using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] EnemySettings _enemySettingsSO;
    private bool Attacking;
    private int _health;
    private int _damage;
    private EnemyMeleeAttack _enemyMeleeAttack;
    private Transform _transform;
    private PlayerLevelAndStats _playerLevelAndStats;
    private CapsuleCollider _capsuleCollider;
    private SphereCollider _sphereCollider;

    private void Awake()
    {
        _enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _sphereCollider = GetComponent<SphereCollider>();
        _transform = transform;
        _health = _enemySettingsSO.enemyHealth;
        _damage = _enemySettingsSO.enemyDamage;
    }

    private void OnEnable()
    {
        _health = _enemySettingsSO.enemyHealth;
        SetEnableOfColliderComponenets(true);
    }

    private void FixedUpdate()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, _enemySettingsSO.enemyAttackRadius, LayerMask.GetMask("Player"));
        foreach(Collider collider in collisions)
        {
            if(collider.tag == "Player")
            {
                _enemyMeleeAttack.StartAttack(collider.GetComponent<IDamageable>());
                Attacking = true;
            }
        }
        if(collisions.Length == 0)
        {
            _enemyMeleeAttack.StopAttack();
            Attacking = false;
        }
    }

    public void SetPlayerLevelAndStatsReference(PlayerLevelAndStats playerLevelAndStats)
    {
        _playerLevelAndStats = playerLevelAndStats;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(IsItDead())
        {
            StartCoroutine("DeathCoroutine");
        }
    }
    private IEnumerator DeathCoroutine()
    {
        SetEnableOfColliderComponenets(false);
        _playerLevelAndStats.GainExperience(_enemySettingsSO.experience);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);

    }

    private void SetEnableOfColliderComponenets(bool state)
    {
        _capsuleCollider.enabled = state;
        _sphereCollider.enabled = state;
    }

    public bool IsItDead() { return _health <= 0; }
    public bool IsItAttacking() { return Attacking; }
}
