using UnityEngine;

public class PoisonBullet : BaseBullet
{
    public const float poisonDuration = 3f;       // Total time poison will last
    public const float damagePerSecond = 5f;      // How much damage per second

    protected override void HitTarget()
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.ApplyPoison(damagePerSecond, poisonDuration);
           // poisonBullet.Seek(target.transform);
        }

        gameObject.SetActive(false);
    }
}
