using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireBullet : BaseBullet
{
  
    public float damage = 10f;

    public AudioClip fireSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void HitTarget()
    {
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }

        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
