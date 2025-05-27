using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] pathPoints;
    private int targetIndex = 0;

    public EnemyType enemyType;
  
    public float maxHealth;
    private float currentHealth;

 
    private void Start()
    {
        //enemy tower ko btae ke who range me aaya hai    
    }
    void OnEnable()
    {
        SetStatsByType();
        targetIndex = 0;
        currentHealth = maxHealth;
        transform.position = pathPoints[0].position;

        if (pathPoints != null && pathPoints.Length > 0)
        {
            transform.position = pathPoints[0].position;
        }
    }
  
    void Update()
    {
        if (pathPoints == null || targetIndex >= pathPoints.Length) 
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
    public void TakeDamage(float damage)
    {
        //show health baar
        currentHealth -= damage;
         if (currentHealth <= 0)
        {
            Die();
        }
    }
   
    void Die()
    {
        gameObject.SetActive(false);
    }
    public void SetStatsByType()
    {
        switch (enemyType)
        {
            case EnemyType.Slow:
                speed = 1.5f;
                maxHealth = 30f;
                break;
            case EnemyType.Normal:
                speed = 2.5f;
                maxHealth = 50f;
                break;
            case EnemyType.Fast:
                speed = 4.0f;
                maxHealth = 20f;
                break;
            case EnemyType.Tank:
                speed = 1.0f;
                maxHealth = 100f;
                break;
            case EnemyType.Boss:
                speed = 2.0f;
                maxHealth = 200f;
                break;
        }
    }
    public void ResetEnemy()
    {
        targetIndex = 0;
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

