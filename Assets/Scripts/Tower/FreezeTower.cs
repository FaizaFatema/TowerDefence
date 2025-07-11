using static UnityEngine.GraphicsBuffer;
using UnityEngine;

public class FreezeTower : Tower
{
    protected override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        FreezeBullet freezeBullet = bullet.GetComponent<FreezeBullet>();
        if (freezeBullet != null)
            freezeBullet.Seek(target.transform);
    }
}
