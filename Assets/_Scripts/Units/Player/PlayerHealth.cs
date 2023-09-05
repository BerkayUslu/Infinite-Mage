using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerCharacterSettings _playerSettingsSO;
    private bool _playerDied;
    public int _health;

    private void Awake()
    {
        _health = _playerSettingsSO.playerHealth;
        if (_health <= 0) _playerDied = true;
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;

    }
    public bool IsPlayerDead()
    {
        return _playerDied;
    }
}
