using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead = false;
    public Canvas deathCanvas; // optional final UI with RestartButton
    private RestartButton restartButtonScript;

    void Start()
    {
        if (deathCanvas != null)
            restartButtonScript = deathCanvas.GetComponentInChildren<RestartButton>(true);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        LoopManager loopManager = FindObjectOfType<LoopManager>();

        if (loopManager != null && loopManager.currentEchoCount < loopManager.maxEchoCount)
        {
            // ✅ Normal loop reset
            loopManager.ForceStartNextLoop();
            isDead = false; // keep playing
            return;
        }

        // ✅ Final death: restart level
        if (restartButtonScript != null)
        {
            restartButtonScript.RestartScene();
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
