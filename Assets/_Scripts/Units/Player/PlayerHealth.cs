using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PlayerLevelAndStats _playerStats;
    private bool _playerDied;
    private int _totalHealth;
    private int _currentHealth;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerLevelAndStats>();
    }

    private void Start()
    {
        SetTotalHealth();
        _currentHealth = _totalHealth;
        _playerStats.StatChanged += SetTotalHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Debug.Log("Player Died");
            _playerDied = true;
        }
    }

    private void SetTotalHealth()
    {
        _totalHealth = _playerStats.GetHealthStat();
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetTotalHealth()
    {
        return _totalHealth;
    }

    public bool IsPlayerDead()
    {
        return _playerDied;
    }
}
