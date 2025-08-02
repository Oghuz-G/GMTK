using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Ensure only one MusicManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ✅ Keep playing across scenes
        }
        else
        {
            Destroy(gameObject); // ✅ Prevent duplicate music
        }
    }
}
