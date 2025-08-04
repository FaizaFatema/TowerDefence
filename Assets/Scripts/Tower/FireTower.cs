using static UnityEngine.GraphicsBuffer;
using UnityEngine;

public class FireTower : Tower
{
    protected override void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("FireTower: bulletPrefab or firePoint not assigned!");
            return;
        }

        GameObject bullet = BulletPooler.Instance.SpawnFromPool("FireBullet", firePoint.position, Quaternion.identity);
        BaseBullet baseBullet = bullet.GetComponent<BaseBullet>(); // Changed from bulletPrefab to bullet
        
        if (baseBullet != null && target != null)
        {
            Debug.Log($"FireTower: Shooting bullet at {target.name}");
            baseBullet.Seek(target.transform);
        }
        else
        {
            Debug.LogWarning("FireTower: Failed to shoot - " + 
                (baseBullet == null ? "No BaseBullet component " : "") + 
                (target == null ? "No target" : ""));
            if (bullet != null) Destroy(bullet);
        }
    }
}
