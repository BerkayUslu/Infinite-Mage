using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage);
}

public interface IProjectile
{
    public void SetDirection(Vector3 direction);
    public void SetStats(int damage);
}
