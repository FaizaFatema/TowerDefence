using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameOverManager gameOverManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false); // Optional
            GameManager.Instance.LoseGame();
            gameOverManager.GameOver();
        }
    }
}
