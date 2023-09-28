using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSkillLevelUp : MonoBehaviour
{
    private PlayerSkillManger _skillManager;
    private void Start()
    {
        _skillManager = FindObjectOfType<PlayerSkillManger>();
    }

    public void SkillLevelUp()
    {
        _skillManager.SkillLevelUp("Water Projectile");
    }
}
