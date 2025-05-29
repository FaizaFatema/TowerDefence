using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameOverBackground;
  
    public void GameOver()
    {
        gameOverBackground.SetActive(true);
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        gameOverBackground.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
