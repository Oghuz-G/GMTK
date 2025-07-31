using UnityEngine;
using TMPro;  // Make sure you have TextMeshPro imported

public class TimerUI : MonoBehaviour
{
    public LoopManager loopManager;    // Reference to LoopManager
    public TextMeshProUGUI timerText;  // Assign in Inspector

    void Update()
    {
        if (loopManager != null && timerText != null)
        {
            float timeLeft = loopManager.GetTimeRemaining();
            timerText.text = timeLeft.ToString("F1"); // Show 1 decimal place
        }
    }
}
