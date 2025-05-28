using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (direction.magnitude <= distance)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distance, Space.World);
    }

    void HitTarget()
    {
        EnemyHealth enemyHealth=target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(10f);
        }
        Destroy(gameObject);        // destroy bullet
    }
}
