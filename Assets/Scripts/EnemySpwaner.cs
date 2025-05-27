using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  //  public EnemyPooler EnemyPooler;
    public Transform spawnPoint;            // Jahan se enemy spawn hogi
    public Transform[] pathPoints;          // Waypoints jo enemy follow karegi
    public float spawnInterval = 2f;        // Har kitne second me spawn ho

    private float spawnTimer;

    private EnemyPooler enemyPooler;
   private IEnumerator Start()
   {
    yield return null; // wait one frame
    enemyPooler = FindObjectOfType<EnemyManager>()?.GetPooler();

    if (enemyPooler == null)
        Debug.LogError("EnemyManager or EnemyPooler not found!");
   }
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }
    EnemyType GetRandomEnemyType()
    {
        int rand = Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        return (EnemyType)rand;
    }
    void SpawnEnemy()
    {

        EnemyType randomType = GetRandomEnemyType();
        GameObject enemy = enemyPooler.GetEnemy(randomType);

        if (enemy == null)
        {
            Debug.LogError("Enemy is null");
            return;
        }

        enemy.transform.position = spawnPoint.position;

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript == null)
        {
            Debug.LogError("Enemy script missing on prefab");
            return;
        }

        if (pathPoints == null || pathPoints.Length == 0)
        {
            Debug.LogError("Path points not assigned to EnemySpawner");
            return;
        }


        if (enemyScript != null)
        {
            enemyScript.enemyType = randomType;
            enemyScript.pathPoints = pathPoints;  // Waypoints assign karo
            enemyScript.SetStatsByType();   
            enemyScript.ResetEnemy();// Reset logic
        }
       
        enemy.SetActive(true);  
    }
   
}
