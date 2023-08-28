using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameplayVirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] GameObject _playerGameObject;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        Transform playerTransform = _playerGameObject.transform.GetChild(0).transform;

        if (_virtualCamera.Follow == null)
        {
            _virtualCamera.Follow = playerTransform;
        }
        if (_virtualCamera.LookAt == null)
        {
            _virtualCamera.LookAt = playerTransform;
        }
    }


}
