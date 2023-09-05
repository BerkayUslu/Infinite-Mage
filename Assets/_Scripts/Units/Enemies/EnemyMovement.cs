using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rb;
    private Vector3 _playerLastSeenPosition;
    private bool SawThePlayer = false;
    [SerializeField] EnemySettings enemySettings;

    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine("EnemyVisionCoroutine");
    }

    private void Update()
    {
        Rotate();
        if (!SawThePlayer) return;
        Walk();
    }

    private void Walk()
    {
        Vector3 walkDirection = _playerLastSeenPosition - _transform.position;
        _rb.velocity = walkDirection * enemySettings.enemySpeed;
    }

    private void Rotate()
    {
        Vector3 walkDirection = _playerLastSeenPosition - _transform.position;
        if (walkDirection == Vector3.zero) return;
        Quaternion lookRotation = Quaternion.LookRotation(walkDirection);
        _transform.rotation = lookRotation;
    }

    private IEnumerator EnemyVisionCoroutine()
    {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, enemySettings.enemyVisionRadius, LayerMask.GetMask("Player"));
            foreach(Collider collider in colliders)
            {
                SawThePlayer = true;
                _playerLastSeenPosition = collider.transform.position;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
