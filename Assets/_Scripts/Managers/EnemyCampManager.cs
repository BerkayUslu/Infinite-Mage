using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyCampManager : MonoBehaviour
{
    
    public Action<Vector3> PlayerSpotted;
    private Transform _playerTransform;
    [SerializeField] EnemyCampSettings _enemyCampSettingSO;

    //Camp spawning variables
    [SerializeField] int _countOfRings;
    [SerializeField] List<int> _countOfCampsInRingsInToOut;
    private List<float> _ringRadiusList;
    private const float TOTAL_RADIUS = 450;
    private Vector3 _center = new Vector3(500, 0, 500);
    private int _minDistanceToNextCamp = 60;
    private int _maxDistanceToNextCamp = 200;

    private Transform _enemyCampsObjectTransform;

    //Bütün kampların ayarlarını depolamak için bir liste
    [SerializeField] List<EnemyCampSettings> _enemyCampSettingsSOList;
    //Bütün oluşturulmuş kampları, bunların levelini ve konumunu tuttuğumuz bir değişken
    [SerializeField] List<EnemyCampClass> _enemyCampsList;

    private void Awake()
    {
        _ringRadiusList = new List<float>();
    }
    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().transform;
        _enemyCampsObjectTransform = GameObject.Find("Enemy Camps").transform;
        CampSpawnProcess();
    }

    private void SpawnCamp(Vector3 position)
    {
        GameObject camp = Instantiate(_enemyCampSettingSO.campPrefab, position, _enemyCampSettingSO.campPrefab.transform.rotation);
        EnemyCamp tempCampClass = camp.GetComponent<EnemyCamp>();
        camp.transform.SetParent(_enemyCampsObjectTransform);
        _enemyCampsList.Add(new EnemyCampClass(position, 1, _enemyCampSettingSO));
        tempCampClass.SetVariables(_enemyCampSettingSO, _playerTransform);
        tempCampClass.CampSpottedPlayer += PlayerSpotted;
    }




    // Spawn the camps in the map according to the "type", "level", and "where"
    // I am thinking to use certain monsters for certain levels like 1 to 5 is ogre, 5 to 10 ogre and something mixed
    // Merkezden başlayarak halkalar halinde farklı level alanları yaratmayı düşünüyorum.
    // Merkezde en güçlüler, dış halkalara hareket ettikçe daha güçsüzler.
    // !!!! önceki kamp lokasyonuna göre yeni kamplar eklediğimiz kod yazılacak
    private void CampSpawnProcess()
    {
        CalculateRingsRadius();
        Vector3? previousCampLocation = null;
        for (int currentRing = 0; currentRing < _countOfRings; currentRing++)
        {
            for(int camp = 0; camp < _countOfCampsInRingsInToOut[currentRing]; camp++)
            {
                previousCampLocation = GenerateNewCampPosition(currentRing, previousCampLocation);
                SpawnCamp(previousCampLocation.Value);
            }
            previousCampLocation = null; 
        }
    }

    private Vector3? GenerateNewCampPosition(int ringIndexToSpawnIn, Vector3? previousCampLocation)
    {
        float randomAngleRadians;
        float randomRadius;
        float xPos;
        float zPos;

        while (true)//previousCampLocation == null)
        {
            randomAngleRadians = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
            randomRadius = UnityEngine.Random.Range(_ringRadiusList[ringIndexToSpawnIn], _ringRadiusList[ringIndexToSpawnIn + 1]);
            xPos = randomRadius * Mathf.Cos(randomAngleRadians);
            zPos = randomRadius * Mathf.Sin(randomAngleRadians);
            Vector3 positionDifferenceVector = new Vector3(xPos, 0, zPos);
            Vector3 newCampPosition = _center + positionDifferenceVector;
            if (CheckIfTheNewCampLocationIsAvailable(newCampPosition))
            {
                return newCampPosition;
            }
        }

    }

    private bool CheckIfTheNewCampLocationIsAvailable(Vector3 campLocation)
    {
        foreach(EnemyCampClass enemyCamp in _enemyCampsList)
        {   
            if(Vector3.Distance(campLocation, enemyCamp._campLocation) < _minDistanceToNextCamp || (campLocation.x > 950) || (campLocation.z > 950) || (campLocation.x < 50) || (campLocation.z < 50) )
            {
                return false;
            }
        }
        return true;
    }

    private void CalculateRingsRadius()
    {
        float center = 0f;
        _ringRadiusList.Add(center);
        for (int i = 1; i <= _countOfRings; i++)
        {
            _ringRadiusList.Add(TOTAL_RADIUS / _countOfRings * (i + 1)) ;
        }
    }
}

[Serializable]
public class EnemyCampClass
{
    public Vector3 _campLocation;
    public int _campLevel;
    public GameObject _campGameObject; 
    public EnemyCampSettings _campSettingsSO;

    public EnemyCampClass(Vector3 location, int level, EnemyCampSettings settings)
    {
        _campLocation = location;
        _campLevel = level;
        _campSettingsSO = settings;
    }
}