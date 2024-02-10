using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFishEnemy : MonoBehaviour, IDamageable
{
    private int _fishDamage = 2 ;
    private int _health = 5;

    private void Start()
    {

    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Attack(IDamageable player)
    {
        player.TakeDamage(_fishDamage);
    }
}
