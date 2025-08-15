
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
    public static EnemyPooler Instance;

    void Awake()
    {
        Instance = this;
    }
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
           // queue.Enqueue(enemy); // Put it back at end

            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
            queue.Enqueue(enemy);
        }
        return null; // safety
    }
    public void ReturnEnemyToPool(EnemyType type, GameObject enemy)
    {
        enemy.SetActive(false);
        if (enemyQueues.ContainsKey(type))
        {
            enemyQueues[type].Enqueue(enemy);
        }
    }
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
