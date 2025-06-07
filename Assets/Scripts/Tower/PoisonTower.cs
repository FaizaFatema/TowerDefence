using UnityEngine;

public class PoisonTower : Tower
{
    protected override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        PoisonBullet poisonBullet = bullet.GetComponent<PoisonBullet>();
        if (poisonBullet != null)
            poisonBullet.Seek(target.transform);
    }
}
