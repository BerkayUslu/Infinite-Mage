using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerLevelAndStats : MonoBehaviour
{
    private const string DAMAGE = "PlayerDamage";
    private const string HEALTH = "PlayerHealth";
    private const string SPEED = "PlayerSpeed";
    private const string EXPERIENCE = "ExperiencePoints";
    private const string LEVEL = "PlayerLevel";
    private const string COOLDOWN = "PlayerCooldown";

    [Header("Player Settings")]
    [SerializeField] PlayerCharacterSettings _playerSettings;
    [Header("Player Current Stats")]
    [SerializeField] int _playerLevel;
    [SerializeReference] int _experiencePoints;
    [SerializeField] float _speedStat;
    [SerializeField] int _damageStat;
    [SerializeField] int _healthStat;
    [SerializeField] float _skillCooldownStat;
    [Header("Stat Increase Rate")]
    [SerializeField] float _speedIncreasePercentage;
    [SerializeField] int _damageIncrease;
    [SerializeField] int _healthIncrease;
    [SerializeField] float _cooldownDecreasePercentage;

    public Action StatsChanged;



    private void Start()
    {
        LoadPlayerStats();
    }

    public void GainExperience(int experiencePoints)
    {
        _experiencePoints += experiencePoints;
        CheckPlayerExperienceAndLevelUp();
    }

    private void CheckPlayerExperienceAndLevelUp()
    {
        // Open the stat level up UI
        _playerLevel++;
        PlayerPrefs.SetInt(LEVEL, _playerLevel);
        
    }

    public void IncreaseStat(StatTypes stat)
    {
        switch (stat)
        {
            case StatTypes.Damage:
                _damageStat += _damageIncrease;
                PlayerPrefs.SetInt(DAMAGE, _damageStat);
                break;
            case StatTypes.Health:
                _healthStat += _healthIncrease;
                PlayerPrefs.SetInt(HEALTH, _healthStat);
                break;
            case StatTypes.Cooldown:
                _skillCooldownStat += _skillCooldownStat * _cooldownDecreasePercentage;
                PlayerPrefs.SetFloat(COOLDOWN, _skillCooldownStat);
                break;
            case StatTypes.Speed:
                _speedStat += _speedStat * _speedIncreasePercentage;
                PlayerPrefs.SetFloat(SPEED, _speedStat);
                break;
        }
        StatsChanged.Invoke();
    }

    public int GetDamageStat() { return _damageStat; }
    public int GetHealthStat() { return _healthStat; }
    public float GetSpeedStat() { return _speedStat; }
    public float GetCooldownStat() { return _skillCooldownStat; }

    private void LoadPlayerStats()
    {
        _experiencePoints = PlayerPrefs.GetInt(EXPERIENCE, 0);
        _playerLevel = PlayerPrefs.GetInt(LEVEL, 0);
        _speedStat = PlayerPrefs.GetFloat(SPEED, _playerSettings.playerSpeed);
        _damageStat = PlayerPrefs.GetInt(DAMAGE, _playerSettings.playerDamage);
        _healthStat = PlayerPrefs.GetInt(HEALTH, _playerSettings.playerHealth);
        _skillCooldownStat = PlayerPrefs.GetFloat(COOLDOWN, _playerSettings.playerbaseSkillCooldown);
    }

    public enum StatTypes
    {
        Health,
        Damage,
        Speed,
        Cooldown
    }
}
