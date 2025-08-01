using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [Header("References")]
    public GameObject levelInfoPanel;      // Assign Panel_LevelInfo
    public GameObject mainMenuPanel;       // Assign Panel_MainMenu
    public UnityEngine.UI.Button[] levelButtons; // Explicitly UI button

    void Start()
    {
        if (levelInfoPanel != null)
            levelInfoPanel.SetActive(false);

        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = (i < unlockedLevels);
        }
    }

    public void OnLevelButtonClicked(int levelIndex)
    {
        if (levelInfoPanel == null) return;

        gameObject.SetActive(false);
        levelInfoPanel.SetActive(true);

        LevelInfoPanel infoPanel = levelInfoPanel.GetComponent<LevelInfoPanel>();
        if (infoPanel != null)
            infoPanel.ShowLevel(levelIndex);
    }

    public void OnBackButton()
    {
        gameObject.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }
}
