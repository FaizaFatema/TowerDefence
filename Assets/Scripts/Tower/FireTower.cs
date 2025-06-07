using static UnityEngine.GraphicsBuffer;
using UnityEngine;

public class FireTower : Tower
{
    protected override void Shoot()
    {
       base.Shoot();

        // Additional functionality for FireTower
        FireBullet fireBullet = bulletPrefab.GetComponent<FireBullet>();
        if (fireBullet != null)
        {
            fireBullet.damage *= 1.5f; // Increase damage for FireTower
        }
    }
}
