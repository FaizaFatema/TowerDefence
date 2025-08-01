
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] pathPoints;
    private int targetIndex = 0;

    public EnemyType enemyType;

    public EnemyHealth enemyHealth;

    private bool isFrozen = false;
    private float freezeTimer = 0f;
    void OnEnable()
    {
        if (pathPoints == null || pathPoints.Length == 0)
            return;


        targetIndex = 0;
        //    transform.position = pathPoints[0].position;
    }
    void Update()
    {
        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0f)
            {
                isFrozen = false;
            }
            return; // frozen hai to move nahi karega
        }

        if (targetIndex >= pathPoints.Length)
        {
            gameObject.SetActive(false);
            return;
        }

        Transform targetPoint = pathPoints[targetIndex];
        Vector2 dir = targetPoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetIndex++;
        }
    }

    public void SetStatsByType()
    {
        switch (enemyType)
        {
            case EnemyType.Slow:
                speed = 1.5f;
                enemyHealth.maxHealth = 50f;
                break;
            case EnemyType.Normal:
                speed = 2.5f;
                enemyHealth.maxHealth = 30f;
                break;
            case EnemyType.Fast:
                speed = 4.0f;
                enemyHealth.maxHealth = 20f;
                break;
            case EnemyType.Tank:
                speed = 1.0f;
                enemyHealth.maxHealth = 100f;
                break;
            case EnemyType.Boss:
                speed = 2.0f;
                enemyHealth.maxHealth = 200f;
                break;
        }
    }
    public void ResetEnemy()
    {
        targetIndex = 0;
    }
    public void Freeze(float duration)
    {
        isFrozen = true;
        freezeTimer = duration;
    }
}
public enum EnemyType
{
    Slow,
    Normal,
    Fast,
    Tank,
    Boss
}
