using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [Header("Laser Settings")]
    public float maxDistance = 20f;          // How far the laser can go
    public float killDelay = 1.0f;           // Time before the player dies
    public LayerMask collisionMask;          // What blocks the laser
    public Color laserColor = Color.red;
    public float width = 0.1f;

    private LineRenderer lineRenderer;
    private PlayerDeath playerInLaser = null;
    private float timer = 0f;

    void Start()
    {
        // Setup line renderer for the laser
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Cast a ray forward (local right = laser direction)
        Vector2 origin = transform.position;
        Vector2 direction = transform.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, collisionMask);

        Vector2 endPoint = origin + direction * maxDistance;

        if (hit.collider != null)
        {
            endPoint = hit.point;

            // Check for player
            if (hit.collider.CompareTag("Player"))
            {
                if (playerInLaser == null)
                {
                    playerInLaser = hit.collider.GetComponent<PlayerDeath>();
                    timer = 0f;
                }
            }
            else
            {
                playerInLaser = null; // reset if not hitting player
                timer = 0f;
            }
        }
        else
        {
            playerInLaser = null;
            timer = 0f;
        }

        // Update line renderer to laser end
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, endPoint);

        // Handle delayed kill
        if (playerInLaser != null)
        {
            timer += Time.deltaTime;
            if (timer >= killDelay)
            {
                playerInLaser.Die();
                playerInLaser = null;
            }
        }
    }
}
