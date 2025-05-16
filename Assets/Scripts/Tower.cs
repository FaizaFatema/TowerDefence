using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab;     // Bullet ka prefab (template)
    public Transform firePoint;         // Jahan se bullet niklegi

    public float fireRate = 1f;         // Kitni der baad fire kare
    private float fireTimer = 0f;       // Countdown timer
    public float range = 3f;

    private GameObject target;          // Jo enemy currently target hai

    void Update()
    {
        FindNearestEnemy();
        // Agar koi enemy range mein hai
        if (target != null)
        {
           // Debug.Log("Target in range: " + target.name);
            // Agar time ho gaya fire karne ka
            if (fireTimer <= 0f)
            {
                Shoot();               // Bullet chalao
                fireTimer = fireRate;  // Timer reset karo
            }

            fireTimer -= Time.deltaTime;  // Time kam karte jao har frame
        }
    }
    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");// Saare enemies scene se le lo jinka tag "Enemy" hai
        float shortestDistance = Mathf.Infinity;// Sabse chhoti distance ko initially Infinity set karte hain
        GameObject nearestEnemy = null;// Sabse kareeb enemy ko temporarily null rakhte hain
        for (int i = 0; i < enemies.Length; i++)
        {
        
            GameObject enemy = enemies[i];

            // Tower aur is enemy ke beech ka distance nikaalo
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            // Check karo: kya ye enemy ab tak ki sabse paas hai?
            // Aur kya ye tower ke range ke andar hai?
            // Aur kya ye active bhi hai? (i.e. disable nahi hui pooling me)
            if (distance < shortestDistance && distance <= range && enemy.activeInHierarchy)
            {
                // To ye abhi tak ki sabse kareeb enemy hai
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        // Loop ke baad, sabse kareeb enemy ko target set kar do
        target = nearestEnemy;
    }
    void Shoot()
    {
        // Bullet create karo firePoint position se
        GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        // Agar bullet script mila to usko target de do
        if (bullet != null)
            bullet.Seek(target.transform);
    }

    void OnDrawGizmosSelected()
    {
        // Yellow color set karo gizmo ke liye
        Gizmos.color = Color.yellow;

        // Tower ke center se ek circle draw karo (2D me radius = range)
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
