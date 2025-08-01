using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;            // Jahan se enemy spawn hogi
    public Transform[] pathPoints;          // Waypoints jo enemy follow karegi
    private EnemyPooler enemyPooler;

    private void Start()
    {
        enemyPooler = FindObjectOfType<EnemyPooler>();
    }

    // Ye method WaveManager call karega
    public void SpawnEnemy(EnemyType type)
    {
        GameObject enemy = enemyPooler.GetEnemy(type);

        if (enemy == null) return;

        Enemy enemyScript = enemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.enemyType = type;
            enemyScript.pathPoints = pathPoints;  // Waypoints assign karo
            enemyScript.SetStatsByType();
            enemyScript.ResetEnemy(); // Reset logic
        }

        enemy.transform.position = spawnPoint.position;
        enemy.SetActive(true);
    }
}
