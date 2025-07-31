using System.Collections.Generic;
using UnityEngine;

public class EchoPlayback : MonoBehaviour
{
    private List<Vector3> positions;
    private List<bool> jumps;
    private int frameIndex = 0;
    private bool isPlaying = true;

    private Rigidbody2D rb;

    // ✅ Initialize from PlayerRecorder
    public void InitializeFromRecorder(PlayerRecorder recorder)
    {
        if (recorder == null) return;

        positions = new List<Vector3>(recorder.recordedPositions);
        jumps = new List<bool>(recorder.recordedJumps);

        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true; // Disable physics
            rb.linearVelocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (!isPlaying || positions == null || frameIndex >= positions.Count)
            return;

        // Move to the next recorded position
        transform.position = positions[frameIndex];
        frameIndex++;

        // If finished playback, freeze in place
        if (frameIndex >= positions.Count)
        {
            FreezeEcho();
        }
    }

    void FreezeEcho()
    {
        isPlaying = false;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
