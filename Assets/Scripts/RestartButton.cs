using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartScene()
    {
        Time.timeScale = 1f; // Resume
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
