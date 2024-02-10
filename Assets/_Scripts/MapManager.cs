using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Transform[] _mapLocations;
    [SerializeField] Transform _playerTransform;



    private void LateUpdate()
    {
        //Horizontal map movement
        if(_playerTransform.position.x > _mapLocations[1].position.x + 500)
        {
            _mapLocations[0].position = _mapLocations[0].position + 2000 * Vector3.right;
            _mapLocations[2].position = _mapLocations[2].position + 2000 * Vector3.right;

            Transform tempOne = _mapLocations[1];
            Transform tempThree = _mapLocations[3];

            _mapLocations[1] = _mapLocations[0];
            _mapLocations[3] = _mapLocations[2];
            _mapLocations[0] = tempOne;
            _mapLocations[2] = tempThree;
        }
        else if(_playerTransform.position.x < _mapLocations[1].position.x - 500)
        {
            _mapLocations[1].position = _mapLocations[1].position - 2000 * Vector3.right;
            _mapLocations[3].position = _mapLocations[3].position - 2000 * Vector3.right;

            Transform tempZero = _mapLocations[0];
            Transform tempTwo = _mapLocations[2];

            _mapLocations[0] = _mapLocations[1];
            _mapLocations[2] = _mapLocations[3];
            _mapLocations[1] = tempZero;
            _mapLocations[3] = tempTwo;
        }

        //Vertical map movement
        if (_playerTransform.position.z > _mapLocations[1].position.z + 500)
        {
            _mapLocations[2].position = _mapLocations[2].position + 2000 * Vector3.forward;
            _mapLocations[3].position = _mapLocations[3].position + 2000 * Vector3.forward;

            Transform tempZero = _mapLocations[0];
            Transform tempOne = _mapLocations[1];

            _mapLocations[0] = _mapLocations[2];
            _mapLocations[1] = _mapLocations[3];
            _mapLocations[2] = tempZero;
            _mapLocations[3] = tempOne;
        }
        else if (_playerTransform.position.z < _mapLocations[1].position.z - 500)
        {
            _mapLocations[0].position = _mapLocations[0].position - 2000 * Vector3.forward;
            _mapLocations[1].position = _mapLocations[1].position - 2000 * Vector3.forward;

            Transform tempTwo = _mapLocations[2];
            Transform tempThree = _mapLocations[3];

            _mapLocations[2] = _mapLocations[0];
            _mapLocations[3] = _mapLocations[1];
            _mapLocations[0] = tempTwo;
            _mapLocations[1] = tempThree;
        }
    }

}
