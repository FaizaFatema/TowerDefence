
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
[System.Serializable]
public class EnemyPoolInfo
{
    public EnemyType type;
    public string prefabName;
    public int totalInPool;   // runtime me count
}
public class EnemyPooler : MonoBehaviour
{

    public List<EnemyTypePrefab> enemyPrefabs;

    public const int poolSize = 5;


    private Dictionary<EnemyType, Queue<GameObject>> enemyQueues = new Dictionary<EnemyType, Queue<GameObject>>();
    public static EnemyPooler Instance;

    [Header("Enemy Pool Info (Runtime ReadOnly)")]
    public List<EnemyPoolInfo> poolInfo = new List<EnemyPoolInfo>();


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
        UpdatePoolInfo();
    }
    void Update()
    {
        UpdatePoolInfo(); // har frame runtime count update
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
    private void UpdatePoolInfo()
    {
        poolInfo.Clear();

        foreach (var entry in enemyPrefabs)
        {
            if (!enemyQueues.ContainsKey(entry.type)) continue;

            poolInfo.Add(new EnemyPoolInfo
            {
                type = entry.type,
                prefabName = entry.prefab != null ? entry.prefab.name : "Null",
                totalInPool = enemyQueues[entry.type].Count
            });
        }
    }
}
