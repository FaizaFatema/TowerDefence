using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FreezeBullet : BaseBullet
{
    public float freezeDuration = 2f;

    protected override void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Freeze(freezeDuration);
        }

        gameObject.SetActive(false);
    }
}
