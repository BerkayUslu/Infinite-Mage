using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    private int _playerDamageStat;
    private int _playerCooldownStat;
    private Transform _transform;
    private PlayerLevelAndStats _playerStats;
    private Vector3 _directionAdjustemntVector;

    private List<Skill> debugSkillList;

    private void Awake()
    {
        debugSkillList = new List<Skill>();
        _playerStats = GetComponent<PlayerLevelAndStats>();
        _transform = transform;
        _directionAdjustemntVector = new Vector3(1, 0, 1);
    }

    private void Start()
    {
        GetDamageAndCooldownStats();
        _playerStats.StatChanged += GetDamageAndCooldownStats;
    }

    public void AddNewSkillToCast(Skill skill)
    {
        StartCoroutine("CastSpellWithInterval", skill);
        debugSkillList.Add(skill);
    }

    private void GetDamageAndCooldownStats()
    {
        _playerDamageStat = _playerStats.GetDamageStat();
        _playerCooldownStat = _playerStats.GetCooldownStat();
    }

    private void CastProjectileSkill(Skill skill)
    {
        Collider shortestDistanceEnemy = null;
        float shortestDistance = Mathf.Infinity;
        Collider[] colliders = Physics.OverlapSphere(_transform.position, 15, LayerMask.GetMask("Enemy"));
        foreach(Collider collider in colliders)
        {
            float distance = Vector3.Distance(_transform.position, collider.transform.position);
            if(distance < 4)
            {
                shortestDistanceEnemy = collider;
                break;
            }
            if (distance < shortestDistance)
            {
                shortestDistanceEnemy = collider;
            }
        }
        if (shortestDistanceEnemy == null) return;
        Vector3 directionToShoot = snail.CustomMath.ElementwiseVectorMultiply((shortestDistanceEnemy.transform.position - _transform.position).normalized, _directionAdjustemntVector);
        GameObject projectile = Instantiate(skill._skillSettings.skillPrefab, _transform.position + Vector3.up / 2, Quaternion.identity);
        IProjectile projectileInterface = projectile.GetComponent<IProjectile>();
        projectileInterface.SetDirection(directionToShoot);
        projectileInterface.SetStats(skill._damage + _playerDamageStat);

    }

    private void CastAOESkill() { }

    private void CastTargetFocusedSkill() { }

    private IEnumerator CastSpellWithInterval(Skill skill)
    {
        while (true)
        {
            switch (skill._skillSettings.skillType)
            {
                case PlayerSkillSettings.SkillTypes.projectile:
                    CastProjectileSkill(skill);
                    break;
                case PlayerSkillSettings.SkillTypes.areaOfEffect:
                    CastAOESkill();
                    break;
                case PlayerSkillSettings.SkillTypes.targetFocused:
                    CastTargetFocusedSkill();
                    break;
            }
            yield return new WaitForSeconds(skill._cooldownTime - (_playerCooldownStat * skill._cooldownTime / 100));
        }
    }

    //private void SkillDebug()
    //{
    //    GameObject projectile = Instantiate(debugSkillList[0]._skillSettings.skillPrefab, _transform.position + Vector3.up / 2, Quaternion.identity);
    //    IProjectile projectileInterface = projectile.GetComponent<IProjectile>();
    //    projectileInterface.SetDirection(Vector3.right);
    //    projectileInterface.SetStats(debugSkillList[0]._damage + _playerDamageStat);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        SkillDebug();
    //    }
    //}
}
