using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;          // Enemy ka prefab
    public Transform spawnPoint;            // Jahan se enemy spawn hogi
    public Transform[] pathPoints;          // Waypoints jo enemy follow karegi
    public float spawnInterval = 2f;        // Har kitne second me spawn ho

    private float spawnTimer;

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.pathPoints = pathPoints;  // Waypoints assign karo
        }
    }
   
}
