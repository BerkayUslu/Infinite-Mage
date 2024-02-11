using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PlayerLevelAndStats _playerStats;
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] GameObject _gameplayUI;
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
            _playerDied = true;
            _gameOverUI.SetActive(true);
            _gameplayUI.SetActive(false);
            snail.ManageGame.PauseGame(true);
        }
    }

    private void SetTotalHealth()
    {
        _totalHealth = _playerStats.GetHealthStat();
    }

    public void GainHealth()
    {
        if (_currentHealth >= _totalHealth)
            return;
        _currentHealth++;
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
