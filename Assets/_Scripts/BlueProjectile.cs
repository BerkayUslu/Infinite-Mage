using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueProjectile : MonoBehaviour, IProjectile
{
    private Vector3 _projectileDirection;
    private bool _directionSetFlag;
    private float _projectileSpeed = 15;
    private int _damage;
    private Transform _transform;

    private float _birthTime;

    private void Awake()
    {
        _transform = transform;
        _birthTime = Time.time;
    }
    private void OnDisable()
    {
        _directionSetFlag = false;
    }

    public void SetDirection(Vector3 direction)
    {
        _projectileDirection = direction;
        _directionSetFlag = true;
    }

    public void SetStats(int damage)
    {
        _damage = damage;
    }

    private void Update()
    {
        if (_directionSetFlag)
        {
            _transform.Translate(_projectileDirection * Time.deltaTime * _projectileSpeed);
            if(_projectileSpeed > 3)
            {
                _projectileSpeed -= 0.02f;
            }
        }
        if(Time.time > _birthTime + 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        other.gameObject.GetComponentInParent<IDamageable>()?.TakeDamage(_damage);
        Destroy(gameObject);
    }


}
