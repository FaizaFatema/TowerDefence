using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI endTextUI;
    private bool gameEnded = false;

    void Awake()
    {
        Instance = this;
        endTextUI.gameObject.SetActive(false);
    }

    public void WinGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        endTextUI.text = " You Win! All waves completed!";
        endTextUI.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void LoseGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        endTextUI.text = "You Lost! Enemies broke through!";
        endTextUI.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
}
