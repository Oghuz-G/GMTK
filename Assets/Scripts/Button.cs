using UnityEngine;

public class Button : MonoBehaviour
{
    public Door linkedDoor;
    private int objectsOnButton = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player or Echo can trigger
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Echo"))
        {
            objectsOnButton++;
            if (linkedDoor != null)
                linkedDoor.OpenDoor();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Echo"))
        {
            objectsOnButton--;
            if (objectsOnButton <= 0 && linkedDoor != null)
                linkedDoor.CloseDoor();
        }
    }
}
