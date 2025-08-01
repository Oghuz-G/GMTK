using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [Tooltip("The 0-based index of this level (Level 1 = 0, Level 2 = 1, etc.)")]
    public int levelIndex;

    // Called by the button OnClick
    public void OnBackPressed()
    {
        // Save last selected level so LevelInfoPanel knows which info to show
        PlayerPrefs.SetInt("LastSelectedLevel", levelIndex);
        PlayerPrefs.Save();

        // Load MainMenu
        SceneManager.LoadScene("MainMenu");
    }
}
