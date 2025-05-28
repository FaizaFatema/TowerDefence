using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;            // Jahan se enemy spawn hogi
    public Transform[] pathPoints;          // Waypoints jo enemy follow karegi
    public float spawnInterval = 2f;        // Har kitne second me spawn ho

    private float spawnTimer;

    private EnemyPooler enemyPooler;
    private void Start()
    {  
        enemyPooler = FindObjectOfType<EnemyPooler>();
        SpawnEnemy();
        spawnTimer = spawnInterval;
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

        if (enemy == null) return;

        Enemy enemyScript = enemy.GetComponent<Enemy>();

        
        if (enemyScript != null)
        {
            enemyScript.enemyType = randomType;
            enemyScript.pathPoints = pathPoints;  // Waypoints assign karo
            enemyScript.SetStatsByType();   
            enemyScript.ResetEnemy();// Reset logic
        }
        enemy.transform.position = spawnPoint.position;
        enemy.SetActive(true);
    }
   
}
