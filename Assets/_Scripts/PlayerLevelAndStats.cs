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
    private GameObject _statUI;

    [SerializeField] int _neededExperienceToLevelUp;

    [SerializeField] float _speedStat;
    [SerializeField] int _damageStat;
    [SerializeField] int _healthStat;
    [SerializeField] int _skillCooldownStat;

    [Header("Stat Increase Rate")]
    [SerializeField] float _speedIncrease;
    [SerializeField] int _damageIncrease;
    [SerializeField] int _healthIncrease;
    [SerializeField] int _cooldownDecrease;

    public Action StatChanged;



    private void Awake()
    {
        //delete this before build
        PlayerPrefs.DeleteAll();
        LoadPlayerStats();
        CalculateAndSetNeededExperienceToLevelUp();
        _statUI = GameObject.Find("Level Up Stat UI");
        if(_statUI == null) { Debug.Log("Error"); }

    }


    public void GainExperience(int experiencePoints)
    {
        _experiencePoints += experiencePoints;
        CheckPlayerExperienceAndLevelUp();
    }

    private void CheckPlayerExperienceAndLevelUp()
    {
        if (_experiencePoints < _neededExperienceToLevelUp) return;
        _statUI.SetActive(true);
        _playerLevel++;
        CalculateAndSetNeededExperienceToLevelUp();
        PlayerPrefs.SetInt(LEVEL, _playerLevel);
        
    }

    private void CalculateAndSetNeededExperienceToLevelUp()
    {
        _neededExperienceToLevelUp += _playerLevel * 1000;

    }

    public void IncreaseStat(StatTypes stat)
    {
        switch (stat)
        {
            case StatTypes.Damage:
                _damageStat += _damageIncrease;
                PlayerPrefs.SetInt(DAMAGE, _damageStat);
                StatChanged.Invoke();
                break;
            case StatTypes.Health:
                _healthStat += _healthIncrease;
                PlayerPrefs.SetInt(HEALTH, _healthStat);
                StatChanged.Invoke();
                break;
            case StatTypes.Cooldown:
                _skillCooldownStat += _cooldownDecrease;
                PlayerPrefs.SetFloat(COOLDOWN, _skillCooldownStat);
                StatChanged.Invoke();
                break;
            case StatTypes.Speed:
                _speedStat += _speedIncrease;
                PlayerPrefs.SetFloat(SPEED, _speedStat);
                StatChanged.Invoke();
                break;
        }
    }

    public int GetDamageStat() { return _damageStat; }
    public int GetHealthStat() { return _healthStat; }
    public float GetSpeedStat() { return _speedStat; }
    public int GetCooldownStat() { return _skillCooldownStat; }

    private void LoadPlayerStats()
    {
            _experiencePoints = PlayerPrefs.GetInt(EXPERIENCE, 0);
            _playerLevel = PlayerPrefs.GetInt(LEVEL, 1);
            _speedStat = PlayerPrefs.GetFloat(SPEED, _playerSettings.playerSpeed);
            _damageStat = PlayerPrefs.GetInt(DAMAGE, _playerSettings.playerDamage);
            _healthStat = PlayerPrefs.GetInt(HEALTH, _playerSettings.playerHealth);
            _skillCooldownStat = PlayerPrefs.GetInt(COOLDOWN, _playerSettings.playerbaseSkillCooldown);
    }

    public enum StatTypes
    {
        Health,
        Damage,
        Speed,
        Cooldown
    }
}
