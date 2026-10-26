using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused = false;

    void Update()
    {
        // Optional: Press Escape key on PC to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f; // Freezes game time
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resumes game time
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Reset time before switching scenes
        SceneManager.LoadScene("MainMenu"); // Make sure your main menu scene name matches exactly
    }
}