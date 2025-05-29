
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public Image healtFillImage;
    private void OnEnable()
    {
        currentHealth = maxHealth;
        if (healtFillImage != null)
        {
            healtFillImage.fillAmount = 1f;
        }
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healtFillImage != null)
        {
            healtFillImage.fillAmount = currentHealth/maxHealth;
        }
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
