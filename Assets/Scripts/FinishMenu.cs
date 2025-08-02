using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text finishTimeText;
    public GameObject panel;
    public int currentLevelIndex = 0; // 0-based for PlayerPrefs unlocking

    private float startTime;

    void Start()
    {
        // Auto-assign panel to this GameObject if not assigned
        if (panel == null)
            panel = gameObject;

        // Ensure it's off at start
        startTime = Time.time;
    }


    public void ShowFinishMenu()
    {
        Debug.Log("ShowFinishMenu called!");

        if (panel != null)
        {
            panel.SetActive(true);
            Debug.Log("Panel enabled!");
        }
        else
        {
            Debug.LogWarning("Panel reference is missing!");
        }

        float finishTime = Time.time - startTime;
        if (finishTimeText != null)
            finishTimeText.text = "Time: " + finishTime.ToString("F2") + "s";
    }


    // Called by Replay Button
    public void OnReplayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called by Next Level Button
    public void OnNextLevelButton()
    {
        Time.timeScale = 1f;
        int nextIndex = currentLevelIndex + 1;

        if (nextIndex + 1 <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("Level_" + (nextIndex + 1));
        }
        else
        {
            // No more levels, go to menu
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void OnLevelSelectButton()
    {
        Time.timeScale = 1f;

        // Tell the menu to open Level Selection instead of Main Menu
        PlayerPrefs.SetInt("OpenLevelSelection", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");
    }

}