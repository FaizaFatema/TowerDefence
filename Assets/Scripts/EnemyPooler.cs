using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


[System.Serializable]
public class EnemyTypePrefab
{
    public EnemyType type;
    public GameObject prefab;
}

public class EnemyPooler
{
    public List<EnemyTypePrefab> enemyPrefabs;
    public int poolSize = 5;

    private Dictionary<EnemyType, List<GameObject>> enemyPools = new Dictionary<EnemyType, List<GameObject>>();

    // Constructor - Pool setup karta hai
    public EnemyPooler(List<EnemyTypePrefab> prefabs, int size)
    {
        enemyPrefabs = prefabs;
        poolSize = size;
        InitializePools();
    }

    // Har enemy type ke liye pool create karta hai
    public void InitializePools()
    {
        foreach (EnemyTypePrefab entry in enemyPrefabs)
        {
            List<GameObject> pool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = GameObject.Instantiate(entry.prefab);
                obj.SetActive(false);
                pool.Add(obj);
            }
            enemyPools[entry.type] = pool;
        }
    }

    // Pool me se ek inactive enemy object return karta hai
    public GameObject GetEnemy(EnemyType type)
    {
        if (!enemyPools.ContainsKey(type))
            return null;

        foreach (GameObject enemy in enemyPools[type])
        {
            if (!enemy.activeInHierarchy)
                return enemy;
        }

        // Instantiate a new one if pool is exhausted
        EnemyTypePrefab match = enemyPrefabs.Find(p => p.type == type);
        if (match != null)
        {
            GameObject newEnemy = GameObject.Instantiate(match.prefab);
            newEnemy.SetActive(false);
            enemyPools[type].Add(newEnemy);
            return newEnemy;
        }
        return null;
    }
}
