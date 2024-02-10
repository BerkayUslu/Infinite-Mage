using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmMovementController : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    private float _swarmRestInterval = 3;
    private float _restStartTime = -3;
    private Vector3 _targetVector;
    private Transform _transform;
    [SerializeField] float _speed = 10f;
    SwarmStates _swarmCurrentState;

    void Start()
    {
        _transform = transform;
        UpdateTargetVector();
    }

    void LateUpdate()
    {
        if((_playerTransform.position - _transform.position).magnitude < 4)
        {
            _restStartTime = Time.time;
            _swarmCurrentState = SwarmStates.Rest;
        }
        else if(Time.time - _restStartTime > _swarmRestInterval)
        {
            _swarmCurrentState = SwarmStates.Attack;
        }

        if(_swarmCurrentState == SwarmStates.Attack)
        {
            UpdateTargetVector();
        }

        _transform.position += _speed * _targetVector;
    }

    private void UpdateTargetVector()
    {
        _targetVector = _playerTransform.position - _transform.position;
        _targetVector = new Vector3(_targetVector.x, 0, _targetVector.z).normalized;
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (_targetVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_targetVector);
            float yRotation = targetRotation.eulerAngles.y;
            Quaternion newRotation = Quaternion.Euler(0, yRotation, 0);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, newRotation, 0.05f);
        }
    }

    enum SwarmStates
    {
        Attack,
        Rest
    }

}
