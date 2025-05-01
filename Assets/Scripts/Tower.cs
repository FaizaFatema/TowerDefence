using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab;     // Bullet ka prefab (template)
    public Transform firePoint;         // Jahan se bullet niklegi

    public float fireRate = 1f;         // Kitni der baad fire kare
    private float fireTimer = 0f;       // Countdown timer

    private GameObject target;          // Jo enemy currently target hai

    void Update()
    {
        // Agar koi enemy range mein hai
        if (target != null)
        {
            // Agar time ho gaya fire karne ka
            if (fireTimer <= 0f)
            {
                Shoot();               // Bullet chalao
                fireTimer = fireRate;  // Timer reset karo
            }

            fireTimer -= Time.deltaTime;  // Time kam karte jao har frame
        }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        // Agar enemy tower ke range mein aayi
        if (other.CompareTag("Enemy"))
            target = other.gameObject;   // usko target bana lo
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Agar enemy bahar chali gayi
        if (other.CompareTag("Enemy"))
            target = null;              // target hata do
    }
}
