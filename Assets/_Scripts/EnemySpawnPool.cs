using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPool : MonoBehaviour
{
    public static EnemySpawnPool EnemyPoolSharedInstance;
    private Transform _enemyUnitsTransform;
    private PlayerLevelAndStats _playerLevelAndStats;
    private Dictionary<string, List<GameObject>> pooledObjectsDictionaryWithNameKeys;

    private void Awake()
    {
        pooledObjectsDictionaryWithNameKeys = new Dictionary<string, List<GameObject>>();
        _enemyUnitsTransform = GameObject.Find("Enemies").transform;
        EnemyPoolSharedInstance = this;
        _playerLevelAndStats = FindObjectOfType<PlayerLevelAndStats>();
    }


    public GameObject GetPooledObjectOrCreateIfNotAvailable(GameObject objectToPool, string unitName)//maybea some other thing
    {

        if (!pooledObjectsDictionaryWithNameKeys.ContainsKey(unitName))
        {
            pooledObjectsDictionaryWithNameKeys.Add(unitName, new List<GameObject>());
        }

        foreach (GameObject item in pooledObjectsDictionaryWithNameKeys[unitName])
        {
            if (!item.activeInHierarchy)
            {
                return item;
            }
        }

        GameObject newObj = Instantiate(objectToPool);
        newObj.transform.SetParent(_enemyUnitsTransform);
        newObj.GetComponent<Enemy>().SetPlayerLevelAndStatsReference(_playerLevelAndStats);
        newObj.SetActive(false);
        pooledObjectsDictionaryWithNameKeys[unitName].Add(newObj);
        return newObj;

    }
}