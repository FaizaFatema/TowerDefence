using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCompleteUI : MonoBehaviour
{
    public GameObject gameCompletePanel; // assign the panel (parent of text + buttons) here in Inspector

    public void ShowGameComplete()
    {
        gameCompletePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit the game (will only work in build)
        Debug.Log("Game Closed"); // For editor testing
    }
}
