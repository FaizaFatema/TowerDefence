using UnityEngine;

public class PoisonBullet : BaseBullet
{
    public float poisonDuration = 3f;       // Total time poison will last
    public float damagePerSecond = 5f;      // How much damage per second

    protected override void HitTarget()
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.ApplyPoison(damagePerSecond, poisonDuration);
        }

        Destroy(gameObject);
    }
}
