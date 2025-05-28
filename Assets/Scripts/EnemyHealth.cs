using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(float damage)
    {
      
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
