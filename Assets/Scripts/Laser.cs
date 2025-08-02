using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [Header("Laser Settings")]
    public float maxDistance = 20f;
    public float killDelay = 1.0f;

    [Header("Collision Layers")]
    public LayerMask collisionMask;          // Everything that blocks the laser
    public LayerMask playerLayer;            // Only the player layer

    [Header("Visuals")]
    public Color laserColor = Color.red;
    public float width = 0.1f;

    private LineRenderer lineRenderer;
    private PlayerDeath playerInLaser = null;
    private float timer = 0f;
    private bool hasKilledPlayer = false; // ✅ Prevent repeated deaths

    void Start()
    {
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
        Vector2 origin = transform.position;
        Vector2 direction = transform.right;

        // Cast ray to detect first hit
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, collisionMask);

        Vector2 endPoint = origin + direction * maxDistance;
        bool playerHit = false;

        if (hit.collider != null)
        {
            endPoint = hit.point;

            // ✅ Layer-based player detection
            if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
            {
                playerInLaser = hit.collider.GetComponent<PlayerDeath>();
                playerHit = true;
            }
        }

        // Update laser visual
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, endPoint);

        // ✅ Kill player once per entry
        if (playerHit && playerInLaser != null && !hasKilledPlayer)
        {
            timer += Time.deltaTime;

            if (timer >= killDelay)
            {
                playerInLaser.Die();
                hasKilledPlayer = true;
            }
        }
        else
        {
            timer = 0f;

            if (!playerHit)
            {
                playerInLaser = null;
                hasKilledPlayer = false;
            }
        }
    }
}
