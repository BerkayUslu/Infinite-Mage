using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatIncreaseUI : MonoBehaviour
{
    private PlayerLevelAndStats _playerLevelAndStats;
    private GameObject _UI;

    private void Awake()
    {
        _playerLevelAndStats = FindObjectOfType<PlayerLevelAndStats>();
        _UI = GameObject.Find("Level Up Stat UI");
    }

    private void OnEnable()
    {
        snail.ManageGame.PauseGame(true);
    }

    private void OnDisable()
    {
        snail.ManageGame.PauseGame(false);
    }

    private void Start()
    {
        _UI.SetActive(false);
    }

    public void StatIncrease(string statName)
    {
        if(statName == "DAMAGE")
        {
            _playerLevelAndStats.IncreaseStat(PlayerLevelAndStats.StatTypes.Damage);
        }
        else if (statName == "HEALTH")
        {
            _playerLevelAndStats.IncreaseStat(PlayerLevelAndStats.StatTypes.Health);
        }
        else if (statName == "SPEED")
        {
            _playerLevelAndStats.IncreaseStat(PlayerLevelAndStats.StatTypes.Speed);
        }
        else if (statName == "COOLDOWN")
        {
            _playerLevelAndStats.IncreaseStat(PlayerLevelAndStats.StatTypes.Cooldown);
        }

        _UI.SetActive(false);
    }
}
