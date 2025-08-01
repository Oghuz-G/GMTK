using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Movement")]
    public Vector3 openOffset = new Vector3(0f, 3f, 0f);
    public float openSpeed = 3f;

    [Header("Door Collision Settings")]
    public LayerMask obstacleLayers;
    public Vector2 doorCheckSize = new Vector2(0.5f, 0.5f);
    public float doorCheckOffsetY = 0.5f; // distance from center to top/bottom checks

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + openOffset;
    }

    void Update()
    {
        Vector3 targetPos = isOpen ? openPosition : closedPosition;

        // Calculate next step
        Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, openSpeed * Time.deltaTime);

        // Only move if not blocked
        if (!IsPathBlocked(nextPos))
        {
            transform.position = nextPos;
        }
    }

    /// <summary>
    /// Check top and bottom of door at the next position using OverlapBox
    /// </summary>
    private bool IsPathBlocked(Vector3 nextPos)
    {
        Vector3 topCheck = nextPos + new Vector3(0f, doorCheckOffsetY, 0f);
        Vector3 bottomCheck = nextPos + new Vector3(0f, -doorCheckOffsetY, 0f);

        bool hitTop = Physics2D.OverlapBox(topCheck, doorCheckSize, 0f, obstacleLayers);
        bool hitBottom = Physics2D.OverlapBox(bottomCheck, doorCheckSize, 0f, obstacleLayers);

        return hitTop || hitBottom;
    }

    public void OpenDoor() => isOpen = true;
    public void CloseDoor() => isOpen = false;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Determine target open position
        Vector3 currentClosedPosition = Application.isPlaying ? closedPosition : transform.position;
        Vector3 targetOpenPosition = currentClosedPosition + openOffset;

        // Draw path line
        Gizmos.DrawLine(currentClosedPosition, targetOpenPosition);

        // Draw top & bottom check boxes at current position
        Vector3 center = Application.isPlaying ? transform.position : transform.position;
        Vector3 topCheck = center + new Vector3(0f, doorCheckOffsetY, 0f);
        Vector3 bottomCheck = center + new Vector3(0f, -doorCheckOffsetY, 0f);

        Gizmos.DrawWireCube(topCheck, (Vector3)doorCheckSize);
        Gizmos.DrawWireCube(bottomCheck, (Vector3)doorCheckSize);
    }
}
