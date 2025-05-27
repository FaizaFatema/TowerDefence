using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyTypePrefab> enemyPrefabs;
    private EnemyPooler pooler;

    void Start()
    {
        pooler = new EnemyPooler(enemyPrefabs, 5);  // 5 = pool size
    }

    public EnemyPooler GetPooler()
    {
        return pooler;
    }
}
