using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead = false;

    [Header("Death Menu Prefab")]
    public Canvas deathCanvasPrefab; // Assign the Death Menu prefab here

    private LoopManager loopManager;
    private Canvas activeDeathCanvas;

    void Start()
    {
        loopManager = FindObjectOfType<LoopManager>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player died!");

        // ✅ Loop reset if loops remain
        if (loopManager != null && loopManager.currentEchoCount < loopManager.maxEchoCount)
        {
            Debug.Log("Loop reset triggered instead of death menu.");
            loopManager.ForceStartNextLoop();
            isDead = false;
            return;
        }

        // ✅ Final death → spawn death menu
        Time.timeScale = 0f;

        if (deathCanvasPrefab != null)
        {
            Debug.Log("Spawning Death Menu Prefab...");
            activeDeathCanvas = Instantiate(deathCanvasPrefab);
        }
        else
        {
            Debug.LogWarning("DeathCanvasPrefab is not assigned!");
        }
    }
}
