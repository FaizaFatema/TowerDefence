using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] pathPoints;
    private int targetIndex = 0;

    void Update()
    {
        if (targetIndex >= pathPoints.Length) 
        {
            Destroy(gameObject); 
            return; 
        }

        Transform targetPoint = pathPoints[targetIndex];
        Vector2 dir = targetPoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetIndex++;
        }
    }
}
