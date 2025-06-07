using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public Image healtFillImage;

    public GameObject floatingTextPrefab;
    public Transform floatingTextSpawnPoint;

    //private AudioSource audioSource;
    //private bool isDying = false;
    public bool isPoisoned;

    private void OnEnable()
    {
        currentHealth = maxHealth;
      //  isDying = false;

        if (healtFillImage != null)
        {
            healtFillImage.fillAmount = 1f;
        }

       // if (audioSource == null)
         //   audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
       // if (isDying) return;

        currentHealth -= damage;

        if (healtFillImage != null)
        {
            healtFillImage.fillAmount = currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
           Die();
        }
    }

    void Die()
    {
        /* isDying = true;

         if (audioSource != null && audioSource.clip != null)
         {
             audioSource.Play();
             yield return new WaitForSeconds(audioSource.clip.length); // Wait for sound to finish
         }*/
        
        ScoreManager.Instance?.AddScore(10);
        gameObject.SetActive(false);

    }
    public void ApplyPoison(float damagePerSecond, float duration)
    {
        if (!isPoisoned)
            StartCoroutine(PoisonEffect(damagePerSecond, duration));
    }

    private IEnumerator PoisonEffect(float dps, float duration)
    {
        isPoisoned = true;
        float timer = 0f;

        while (timer < duration)
        {
            TakeDamage(dps * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        isPoisoned = false;
    }

}
