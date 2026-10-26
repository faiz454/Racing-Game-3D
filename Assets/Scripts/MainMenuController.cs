using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject garagePanel;

    [Header("Audio Sliders")]
    public Slider sfxVolumeSlider;      // For car / sound effects
    public Slider bgmVolumeSlider;      // For background music

    [Header("Audio Sources (Optional Direct References)")]
    public AudioSource bgmAudioSource;

    void Start()
    {
        ShowMainPanel();

        // Load and apply saved SFX volume
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        // Load and apply saved BGM volume
        if (bgmVolumeSlider != null)
        {
            bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
            bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        }
    }

    // --- Panel Navigation ---
    public void ShowMainPanel()
    {
        if (mainPanel) mainPanel.SetActive(true);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (garagePanel) garagePanel.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        if (mainPanel) mainPanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(true);
        if (garagePanel) garagePanel.SetActive(false);
    }

    public void ShowGaragePanel()
    {
        if (mainPanel) mainPanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (garagePanel) garagePanel.SetActive(true);
    }

    // --- Game Actions ---
    public void PlayGame()
    {
        SceneManager.LoadScene("RacingGame Track");
    }

    public void SetSFXVolume(float value)
    {
        // Adjusts global audio listener volume for sound effects
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetBGMVolume(float value)
    {
        // Adjusts background music source directly
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = value;
        }
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    // --- Garage Color Selection ---
    public void SelectRedColor() { SaveCarColor(Color.red); }
    public void SelectBlueColor() { SaveCarColor(Color.blue); }
    public void SelectGreenColor() { SaveCarColor(Color.green); }
    public void SelectYellowColor() { SaveCarColor(Color.yellow); }
    public void SelectBlackColor() { SaveCarColor(Color.black); }

    void SaveCarColor(Color col)
    {
        PlayerPrefs.SetFloat("CarR", col.r);
        PlayerPrefs.SetFloat("CarG", col.g);
        PlayerPrefs.SetFloat("CarB", col.b);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}