
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

public class EnemyPooler : MonoBehaviour
{

    public List<EnemyTypePrefab> enemyPrefabs;

    public const int poolSize = 5;


    private Dictionary<EnemyType, List<GameObject>> enemyPools = new Dictionary<EnemyType, List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            EnemyTypePrefab entry = enemyPrefabs[i];
            List<GameObject> pool = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(entry.prefab);
                obj.SetActive(false);
                pool.Add(obj);
            }

            enemyPools[entry.type] = pool;
        }

    }
    public GameObject GetEnemy(EnemyType type)
    {
        if (!enemyPools.ContainsKey(type))
            return null;

        List<GameObject> pool = enemyPools[type];

        // pool me dekh lo koi inactive enemy milti hai kya
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i]; // mil gayi to return karo
            }
        }

        // agar koi available nahi thi, to nayi banao, add karo pool me
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            if (enemyPrefabs[i].type == type)
            {
                GameObject newEnemy = Instantiate(enemyPrefabs[i].prefab);
                newEnemy.SetActive(false);
                pool.Add(newEnemy);
                return newEnemy;
            }
        }

        return null; // safety
    }
    public int GetActiveEnemyCount()
    {
        int count = 0;
        foreach (var pool in enemyPools.Values)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].activeInHierarchy)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
