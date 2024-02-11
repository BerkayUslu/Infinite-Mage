using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFishEnemy : MonoBehaviour, IDamageable
{
    private int _fishDamage = 4 ;
    private int _baseHealth = 5;
    [SerializeField] int _health = 5;
    [SerializeField] int _experiencePoints = 100;
    public bool _isAttacking = false;
    private float _lastAttackTime = 0;
    private float _birthTime = 0;
    private PlayerLevelAndStats _playerExperienceClass;
    private IDamageable _playerDamagable;

    private void Start()
    {
        _playerDamagable = FindObjectOfType<PlayerHealth>().GetComponent<IDamageable>();
    }

    private void OnEnable()
    {
        _birthTime = Time.time;
        _health = _baseHealth;
    }

    private void OnDisable()
    {
        _health = _baseHealth;
    }

    private void Update()
    {

        if(_isAttacking && (Time.time - _lastAttackTime > 1))
        {
            Attack();
        }

        if(Time.time - _birthTime > 120)
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            _playerExperienceClass.GainExperience(_experiencePoints);
            gameObject.SetActive(false);
        }
    }

    public void Attack()
    {
        _lastAttackTime = Time.time;
        _playerDamagable?.TakeDamage(_fishDamage);
    }

    public void SetHealthModification(int health)
    {
        _health += health;
    }

    public void SetPlayerExperienceClass(PlayerLevelAndStats experienceClass)
    {
        _playerExperienceClass = experienceClass;
    }

}
