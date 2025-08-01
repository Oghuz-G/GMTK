using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject levelSelectionPanel; // Assign Panel_LevelSelection in Inspector
    public Slider volumeSlider;

    void Start()
    {
        // Always reset time in case game was paused
        Time.timeScale = 1f;

        bool openLevelSelection = PlayerPrefs.GetInt("OpenLevelSelection", 0) == 1;
        PlayerPrefs.SetInt("OpenLevelSelection", 0); // Reset flag

        if (openLevelSelection)
        {
            // Hide Main Menu, Show Level Selection
            if (gameObject != null) gameObject.SetActive(false);
            if (levelSelectionPanel != null) levelSelectionPanel.SetActive(true);
        }
        else
        {
            // Default: show main menu
            gameObject.SetActive(true);
            if (levelSelectionPanel != null) levelSelectionPanel.SetActive(false);
        }

        // Initialize volume slider
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }


    public void OnPlayButton()
    {
        // Hide Main Menu, show Level Selection
        gameObject.SetActive(false);
        if (levelSelectionPanel != null)
            levelSelectionPanel.SetActive(true);
    }

    public void OnQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Quit in editor
#endif
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
