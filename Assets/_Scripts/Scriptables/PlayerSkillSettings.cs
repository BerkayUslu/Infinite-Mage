using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkillSettings", menuName = "ScriptableObjects/Player/Skill Settings")]
public class PlayerSkillSettings : ScriptableObject
{
    public GameObject skillPrefab;
    public SkillTypes skillType;
    public string skillName;
    public int baseDamage;
    public float baseCooldown;
    public int damageIncreaseWithLevel;
    public float cooldownDecreasePrecentageWithLevel;

    public enum SkillTypes
    {
        projectile, // auto aim, there is a chance to miss
        areaOfEffect, // maybea auto or select where it goes
        targetFocused // like lightning strike, guarantees accuracy 
    }
}
