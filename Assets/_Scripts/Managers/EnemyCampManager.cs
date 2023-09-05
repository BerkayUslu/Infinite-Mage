using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyCampManager : MonoBehaviour
{
    public Action<Vector3> PlayerSpotted;
    private Transform _playerTransform;
    [SerializeField] EnemyCampSettings _enemyCampSettingSO;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().transform;
        SpawnCamp();
    }

    private void SpawnCamp()
    {
        GameObject camp = Instantiate(_enemyCampSettingSO.campPrefab, Vector3.zero, _enemyCampSettingSO.campPrefab.transform.rotation);
        EnemyCamp temp = camp.GetComponent<EnemyCamp>();
        temp.SetVariables(_enemyCampSettingSO, _playerTransform);
        temp.CampSpottedPlayer += PlayerSpotted;
    }

}
