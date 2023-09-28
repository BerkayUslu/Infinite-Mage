using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rb;
    private Enemy _enemy;
    private Vector3 _playerLastSeenPosition;
    private bool SawThePlayer = false;
    private EnemyMeleeAttack _enemyMeleeAttack;
    private Coroutine _visionCoroutine;
    [SerializeField] EnemySettings enemySettings;

    private void Awake()
    {
        _transform = transform;
        _enemy = GetComponent<Enemy>();
        _rb = GetComponent<Rigidbody>();
        _enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
    }

    private void OnDisable()
    {
        SawThePlayer = false;
        _rb.velocity = Vector3.zero;
        StopCoroutine(_visionCoroutine);
    }
    private void OnEnable()
    {
          _visionCoroutine = StartCoroutine("EnemyVisionCoroutine");
    }
    private void Start()
    {
        if (_visionCoroutine == null) return;
        _visionCoroutine = StartCoroutine("EnemyVisionCoroutine");
    }

    private void Update()
    {
        if (_enemy.IsItDead()) { _rb.velocity = Vector3.zero; return; }
        Rotate();
        if (!SawThePlayer || _enemy.IsItAttacking())
        {
            _rb.velocity = Vector3.zero;
            return;
        } 
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
