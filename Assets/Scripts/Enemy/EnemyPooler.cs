
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


[System.Serializable]
public class EnemyTypePrefab
{
    public EnemyType type;
    public GameObject prefab;
    public int count;
}

public class EnemyPooler : MonoBehaviour
{

    public List<EnemyTypePrefab> enemyPrefabs;

    public const int poolSize = 5;


    private Dictionary<EnemyType, Queue<GameObject>> enemyQueues = new Dictionary<EnemyType, Queue<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (EnemyTypePrefab entry in enemyPrefabs)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(entry.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            enemyQueues[entry.type] = queue;
        }
    }
    public GameObject GetEnemy(EnemyType type)
    {
        if (!enemyQueues.ContainsKey(type))
            return null;
        Queue<GameObject> queue = enemyQueues[type];

        int size = queue.Count;
        for (int i = 0; i < size; i++)
        {
            GameObject enemy = queue.Dequeue(); // Take from front
            queue.Enqueue(enemy); // Put it back at end

            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return null; // safety
    }

    // agar koi available nahi thi, to nayi banao, add karo pool me
    //for (int i = 0; i < enemyPrefabs.Count; i++)
    //{
    //    if (enemyPrefabs[i].type == type)
    //    {
    //        GameObject newEnemy = Instantiate(enemyPrefabs[i].prefab);
    //        newEnemy.SetActive(false);
    //        pool.Add(newEnemy);
    //        return newEnemy;
    //    }
    //}

    public int GetActiveEnemyCount()
    {
        int count = 0;
        foreach (var queue in enemyQueues.Values)
        {
            foreach (var enemy in queue)
            {
                if (enemy.activeInHierarchy)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
