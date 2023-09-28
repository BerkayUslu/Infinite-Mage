using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowingUI : MonoBehaviour
{
    private Transform _playerTransform;
    private Transform _transform;
    [SerializeField] Vector3 _distanceFromPlayer = new Vector3(0, 1, -2);

    private void Start()
    {
        _playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
        _transform = transform;
    }

    private void LateUpdate()
    {
        _transform.position = _distanceFromPlayer + _playerTransform.position;
    }
}
