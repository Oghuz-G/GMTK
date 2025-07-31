using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathUIPrefab; // Assign prefab in Inspector
    private GameObject spawnedUI;
    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // Disable player movement
        PlayerController2D controller = GetComponent<PlayerController2D>();
        if (controller != null)
            controller.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
        }

        // ✅ Spawn UI before pausing
        if (deathUIPrefab != null && spawnedUI == null)
        {
            spawnedUI = Instantiate(deathUIPrefab);

            // Optional: ensure it uses unscaled time so it appears instantly
            Canvas canvas = spawnedUI.GetComponent<Canvas>();
            if (canvas != null)
                canvas.worldCamera = Camera.main;
        }

        // ✅ Pause game AFTER UI is active
        Time.timeScale = 0f;
    }
}
