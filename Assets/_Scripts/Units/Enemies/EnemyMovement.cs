using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _enemySpeed = 0.01f;
    private Rigidbody _rb;
    private Transform _transform;
    private Vector3 _directionVector;
    private Transform _playetTransform;
    private Vector3 _directionAdjustmentVector = Vector3.forward + Vector3.right;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
        UpdateDirectionVector();
    }

    private void Update()
    {
        UpdateDirectionVector();
        RotationUpdate();
        if (Time.timeScale == 0)
            return;
        _rb.AddForce(_directionVector * _enemySpeed);
    }

    private void UpdateDirectionVector()
    {
        _directionVector = _playetTransform.position - _transform.position;
        _directionVector = Vector3.Scale(_directionVector, _directionAdjustmentVector).normalized;
    }

    private void RotationUpdate()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_directionVector, Vector3.up);
        _transform.rotation = Quaternion.Slerp(_transform.rotation,lookRotation, 0.2f);
    }

    public void SetPlayerTransform(Transform transform)
    {
        _playetTransform = transform;
    }

}
