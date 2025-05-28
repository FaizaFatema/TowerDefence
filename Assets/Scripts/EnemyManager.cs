using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyTypePrefab> enemyPrefabs;
    private EnemyPooler pooler;

    void Awake()
    {
        pooler = new EnemyPooler(enemyPrefabs, 5);  // Pool size
    }

    public EnemyPooler GetPooler()
    {
        return pooler;
    }
}
