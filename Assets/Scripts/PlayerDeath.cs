using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // ✅ Notify loop manager first
        LoopManager loopManager = FindObjectOfType<LoopManager>();
        if (loopManager != null)
        {
            loopManager.ForceStartNextLoop();
        }
    }
}
