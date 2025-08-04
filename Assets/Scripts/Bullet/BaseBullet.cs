using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    protected Transform target;
    protected const float speed = 5f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
          //  Debug.LogWarning("Bullet has no target!");  
            Destroy(gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            HitTarget();
            Debug.Log("Bullet hit target: " + target.name);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    protected abstract void HitTarget(); // Let child bullets define their own logic
}
