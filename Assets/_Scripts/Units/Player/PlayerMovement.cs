using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float _playerMovementSpeed;
    private Rigidbody _rb;
    private PlayerLevelAndStats _playerStats;
    private Transform _transform;
    private Vector2 _movementInputVector2;
    private Vector3 _movementDirectionVector3;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerLevelAndStats>();
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void Start()
    {
        GetMovementSpeed();
        _playerStats.StatChanged += GetMovementSpeed;
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Update()
    {
        _movementDirectionVector3 = new Vector3(_movementInputVector2.x, 0, _movementInputVector2.y);
    }

    private void GetMovementSpeed()
    {
        _playerMovementSpeed = _playerStats.GetSpeedStat();
    }

    private void Rotate()
    {
        if (_movementDirectionVector3 == Vector3.zero) return;
        Quaternion lookRotation = Quaternion.LookRotation(_movementDirectionVector3);
        transform.rotation = lookRotation;
    }
    private void Move()
    {
        _rb.velocity = _movementDirectionVector3 * _playerMovementSpeed;
    }

    public void GetMovementInput(InputAction.CallbackContext context) { _movementInputVector2 = context.ReadValue<Vector2>(); }
}
