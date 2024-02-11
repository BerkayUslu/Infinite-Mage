using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttackTrigger : MonoBehaviour
{
    private AlienFishEnemy _mainClass;

    private void Awake()
    {
        _mainClass = GetComponent<AlienFishEnemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        _mainClass._isAttacking = true;
        _mainClass.Attack();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        _mainClass._isAttacking = false;
    }

}
