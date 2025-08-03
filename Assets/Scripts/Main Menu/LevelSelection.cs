using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons;
    public LevelInfoPanel levelInfoPanel;    // Drag LevelInfo panel
    public GameObject levelSelectionPanel;   // Drag LevelSelection panel
    public GameObject mainMenuPanel;   // Drag LevelSelection panel

    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i; // Local copy for lambda

            levelButtons[i].interactable = true;

            // ✅ Clicking a level shows LevelInfo panel
            levelButtons[i].onClick.AddListener(() =>
            {
                if (levelInfoPanel != null)
                {
                    if (levelSelectionPanel != null)
                        levelSelectionPanel.SetActive(false);

                    levelInfoPanel.gameObject.SetActive(true);
                    levelInfoPanel.ShowLevel(index);
                }
            });
        }
    }

    // ✅ Back button to return to Main Menu
    public void BackToMainMenu()
    {
        levelSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
