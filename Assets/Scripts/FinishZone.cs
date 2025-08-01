using UnityEngine;

public class FinishZone : MonoBehaviour
{
    public FinishMenu finishMenu; // Drag your finish menu script here

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached finish zone!"); // ✅ Debug

            triggered = true;
            finishMenu.ShowFinishMenu();
        }
    }
}
