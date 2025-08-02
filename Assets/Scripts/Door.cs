using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorDirection { Vertical, Horizontal }

    [Header("Door Settings")]
    public DoorDirection doorDirection = DoorDirection.Vertical; // ✅ Choose movement direction
    public float openDistance = 3f; // how far the door moves
    public float openSpeed = 3f;

    [Header("Door Collision Settings")]
    public LayerMask obstacleLayers;
    public Vector2 doorCheckSize = new Vector2(0.5f, 0.5f);
    public float doorCheckOffset = 0.5f; // distance from center to top/bottom OR left/right checks

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.position;

        // Calculate offset based on direction
        Vector3 offset = Vector3.zero;
        if (doorDirection == DoorDirection.Vertical)
            offset = new Vector3(0f, openDistance, 0f);
        else
            offset = new Vector3(openDistance, 0f, 0f);

        openPosition = closedPosition + offset;
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
    /// Check top/bottom or left/right of door at the next position using OverlapBox
    /// </summary>
    private bool IsPathBlocked(Vector3 nextPos)
    {
        Vector3 dirOffset = Vector3.zero;

        // Determine axis to check collisions
        if (doorDirection == DoorDirection.Vertical)
            dirOffset = new Vector3(0f, doorCheckOffset, 0f);
        else
            dirOffset = new Vector3(doorCheckOffset, 0f, 0f);

        Vector3 check1 = nextPos + dirOffset;
        Vector3 check2 = nextPos - dirOffset;

        bool hit1 = Physics2D.OverlapBox(check1, doorCheckSize, 0f, obstacleLayers);
        bool hit2 = Physics2D.OverlapBox(check2, doorCheckSize, 0f, obstacleLayers);

        return hit1 || hit2;
    }

    public void OpenDoor() => isOpen = true;
    public void CloseDoor() => isOpen = false;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Determine target open position
        Vector3 currentClosedPosition = Application.isPlaying ? closedPosition : transform.position;

        Vector3 offset = Vector3.zero;
        if (doorDirection == DoorDirection.Vertical)
            offset = new Vector3(0f, openDistance, 0f);
        else
            offset = new Vector3(openDistance, 0f, 0f);

        Vector3 targetOpenPosition = currentClosedPosition + offset;

        // Draw path line
        Gizmos.DrawLine(currentClosedPosition, targetOpenPosition);

        // Draw collision check boxes
        Vector3 center = Application.isPlaying ? transform.position : transform.position;

        Vector3 dirOffset = (doorDirection == DoorDirection.Vertical)
            ? new Vector3(0f, doorCheckOffset, 0f)
            : new Vector3(doorCheckOffset, 0f, 0f);

        Vector3 check1 = center + dirOffset;
        Vector3 check2 = center - dirOffset;

        Gizmos.DrawWireCube(check1, (Vector3)doorCheckSize);
        Gizmos.DrawWireCube(check2, (Vector3)doorCheckSize);
    }
}
