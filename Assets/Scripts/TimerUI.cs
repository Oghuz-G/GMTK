using UnityEngine;
using TMPro; // For TextMeshPro

public class TimerUI : MonoBehaviour
{
    public LoopManager loopManager;
    public TextMeshProUGUI timerText;  // TMP version

    void Start()
    {
        if (loopManager != null)
        {
            loopManager.OnLoopEnded += HideUI;
        }
    }

    void Update()
    {
        if (loopManager != null)
        {
            float timeRemaining = loopManager.GetTimeRemaining();

            // Show 1 decimal place, e.g., 3.4
            timerText.text = timeRemaining.ToString("F1");
        }

        if (loopManager.currentEchoCount >= loopManager.maxEchoCount)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    private void HideUI()
    {
        gameObject.SetActive(false); // Hide the timer UI
    }

    private void OnDestroy()
    {
        if (loopManager != null)
        {
            loopManager.OnLoopEnded -= HideUI;
        }
    }
}
