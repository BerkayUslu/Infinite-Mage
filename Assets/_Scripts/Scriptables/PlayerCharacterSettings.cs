using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerCharactersettings", menuName = "ScriptableObjects/Player/Player Character Settings")]
public class PlayerCharacterSettings : ScriptableObject
{
    public int playerHealth;
    public int playerDamage;
    public float playerbaseSkillCooldown;
    public float playerSpeed; 
}
