using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelInfoPanel : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text levelTitle;
    public TMP_Text levelDescription;
    public GameObject levelSelectionPanel;  // Assign Panel_LevelSelection in Inspector

    [Header("Level Texts")]
    public string[] levelTitles;        // Assign a title for each level in Inspector
    [TextArea(2, 5)]
    public string[] levelDescriptions;  // Assign a description for each level in Inspector

    private int currentLevelIndex;

    /// <summary>
    /// Show information for the selected level.
    /// </summary>
    public void ShowLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;

        // Set title
        if (levelTitle != null && levelIndex < levelTitles.Length)
            levelTitle.text = levelTitles[levelIndex];
        else if (levelTitle != null)
            levelTitle.text = "Level " + (levelIndex + 1);

        // Set description
        if (levelDescription != null && levelIndex < levelDescriptions.Length)
            levelDescription.text = levelDescriptions[levelIndex];
        else if (levelDescription != null)
            levelDescription.text = "No description for this level yet.";
    }

    /// <summary>
    /// Start the selected level
    /// </summary>
    public void OnStartButton()
    {
        SceneManager.LoadScene("Level_" + (currentLevelIndex + 1));
    }

    /// <summary>
    /// Return to level selection
    /// </summary>
    public void OnBackButton()
    {
        gameObject.SetActive(false);
        if (levelSelectionPanel != null)
            levelSelectionPanel.SetActive(true);
    }
}
