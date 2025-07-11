using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab;     
    public Transform firePoint;         

    public float fireRate = 1f;        
    private float fireTimer = 0f;       
    public float range = 3f;

    protected GameObject target;        

    void Update()
    {
        FindNearestEnemy();
      
        if (target != null)
        {
          
            if (fireTimer <= 0f)
            {
                Shoot();               
                fireTimer = fireRate;  
            }

            fireTimer -= Time.deltaTime;  
        }
    }
    protected virtual void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        BaseBullet bullet = bulletObj.GetComponent<BaseBullet>();
        if (bullet != null && target != null)
        {
            bullet.Seek(target.transform);
        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        for (int i = 0; i < enemies.Length; i++)
        {
        
            GameObject enemy = enemies[i];

          
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance && distance <= range && enemy.activeInHierarchy)
            {
              
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        target = nearestEnemy;
    }
  
    void OnDrawGizmosSelected()
    {
        // Yellow color set karo gizmo ke liye
        Gizmos.color = Color.yellow;

        // Tower ke center se ek circle draw karo (2D me radius = range)
        Gizmos.DrawWireSphere(transform.position, range);
    }
    // Jab tower destroy ho raha ho (jaise wave complete hone par)
    public void DestroyTower()
    {
        // Parent ko dhoondo (jo placement spot hai)
        Transform parentSpot = transform.parent;

        if (parentSpot != null)
        {
            // Parent ka SpriteRenderer wapas enable karo
            SpriteRenderer sr = parentSpot.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = true;
            }
        }

        // Fir tower destroy karo
        Destroy(gameObject);
        TowerLimitManager.Instance.ResetTowerCount();
    }

}
