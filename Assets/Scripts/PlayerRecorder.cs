using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{
    public List<Vector3> recordedPositions = new List<Vector3>();
    public List<bool> recordedJumps = new List<bool>();

    void FixedUpdate() // ✅ use FixedUpdate for consistent playback speed
    {
        recordedPositions.Add(transform.position);
        bool isJumping = Mathf.Abs(GetComponent<Rigidbody2D>().linearVelocity.y) > 0.01f;
        recordedJumps.Add(isJumping);
    }
}
