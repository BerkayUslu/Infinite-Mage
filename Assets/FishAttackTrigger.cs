using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttackTrigger : MonoBehaviour
{
    private AlienFishEnemy _parent;

    private void Awake()
    {
        _parent = GetComponentInParent<AlienFishEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _parent.Attack(other.gameObject.GetComponent<IDamageable>());
    }
}
