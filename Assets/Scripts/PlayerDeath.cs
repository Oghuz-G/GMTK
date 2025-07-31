using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead = false;

    [Header("UI References")]
    public Canvas deathCanvas; // ✅ Drag the whole Death UI Canvas here
    private RestartButton restartButtonScript; // Script that actually restarts the game

    void Start()
    {
        if (deathCanvas != null)
        {
            // Find the RestartButton script anywhere in this Canvas hierarchy
            restartButtonScript = deathCanvas.GetComponentInChildren<RestartButton>(true);
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        LoopManager loopManager = FindObjectOfType<LoopManager>();

        if (loopManager != null)
        {
            if (loopManager.currentEchoCount < loopManager.maxEchoCount)
            {
                // ✅ Trigger next loop instead of death
                loopManager.ForceStartNextLoop();
            }
            else
            {
                // ✅ Directly call the restart script when loops are over
                if (restartButtonScript != null)
                {
                    restartButtonScript.RestartScene();
                }
                else
                {
                    Debug.LogWarning("RestartButton script not found in DeathCanvas!");
                }
            }
        }
    }
}
