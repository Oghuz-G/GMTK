using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(0f, 3f, 0f); // How far the door moves when open
    public float openSpeed = 3f;

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
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * openSpeed);
    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}
