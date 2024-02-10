using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManger : MonoBehaviour
{
    private  List<Skill> _skillList;
    private PlayerSkills _playerSkillsComponent;

    private void Awake()
    {
        _playerSkillsComponent = FindObjectOfType<PlayerSkills>();
        _skillList = new List<Skill>();
    }

    private void Start()
    {
        PlayerSkillSettings[]  _skillSicriptableObjectsArray = snail.SearchAssets.SearchAssetsForScriptableObjectInstances<PlayerSkillSettings>();
        foreach(PlayerSkillSettings playerSkillSettings in _skillSicriptableObjectsArray)
        {
            _skillList.Add(new Skill(playerSkillSettings));
        }

        //starts skill
        SkillLevelUp("Water Projectile");

    }

    public void SkillLevelUp(string skillName)
    {
        foreach(Skill skill in _skillList)
        {
            if(skill._skillSettings.skillName == skillName)
            {
                if(skill._level == 0)
                {
                    _playerSkillsComponent.AddNewSkillToCast(skill);
                }
                skill.SkillLevelUp();
            }
        }
    }
}


[System.Serializable]
public class Skill
{
    public int _level = 0;
    public int _damage;
    public float _cooldownTime;
    public PlayerSkillSettings _skillSettings;
    
    public Skill(PlayerSkillSettings skillSettings)
    {
        _skillSettings = skillSettings;
        _damage = _skillSettings.baseDamage;
        _cooldownTime = _skillSettings.baseCooldown;
    }

    public void SkillLevelUp()
    {
        _level++;
        if (_level == 1) return;
        _damage += _skillSettings.damageIncreaseWithLevel;
        _cooldownTime -= _cooldownTime * _skillSettings.cooldownDecreasePrecentageWithLevel / 100;
    }
}
