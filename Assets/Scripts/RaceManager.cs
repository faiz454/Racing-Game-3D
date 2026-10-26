using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RaceManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private bool raceEnded = false;

    public void EndRace(string message)
    {
        if (raceEnded) return;
        raceEnded = true;

        if (resultPanel != null) resultPanel.SetActive(true);
        if (resultText != null) resultText.text = message;

        Time.timeScale = 0f; // Freeze game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}