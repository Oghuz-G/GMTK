using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;       // Assign your sound
    private AudioSource audioSource;

    void Start()
    {
        // Find AudioSource in Canvas or self
        audioSource = FindObjectOfType<Canvas>().GetComponent<AudioSource>();

        // Subscribe to the button click
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
