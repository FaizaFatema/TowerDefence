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
        // Debug.Log("Hit target with FireBullet");
        //if (fireSound != null && audioSource != null)
        //{
        //    Debug.Log("Playing fire sound");
        //    audioSource.PlayOneShot(fireSound);
        //}
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
