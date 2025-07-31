using UnityEngine;
using System.Collections.Generic;

public class LoopManager : MonoBehaviour
{
    [Header("Loop Settings")]
    public float loopDuration = 5f;       // How long a loop lasts
    public GameObject playerPrefab;       // Reference to the player prefab
    public Transform spawnPoint;          // Where the player respawns each loop

    [Header("Echo Settings")]
    public Material echoMaterial;         // Optional: ghosty material
    public string echoLayerName = "Echo"; // Layer to assign echoes to

    private float loopTimer;
    private GameObject currentPlayer;
    private List<GameObject> echoes = new List<GameObject>();

    void Start()
    {
        loopTimer = loopDuration;

        // Spawn the first player
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        currentPlayer.name = "Player_Live";
    }

    void Update()
    {
        loopTimer -= Time.deltaTime;

        if (loopTimer <= 0f)
        {
            StartNextLoop();
        }
    }

    void StartNextLoop()
    {
        loopTimer = loopDuration;

        // Turn current player into an echo
        GameObject echo = currentPlayer;
        echo.name = "Player_Echo";

        MakeEchoFrozen(echo);

        echoes.Add(echo);

        // Spawn new player for the next loop
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        currentPlayer.name = "Player_Live";
    }

    void MakeEchoFrozen(GameObject echo)
    {
        // 1. Disable player control
        var controller = echo.GetComponent<PlayerController2D>();
        if (controller != null)
            controller.enabled = false;

        // 2. Freeze physics
        Rigidbody2D rb = echo.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;       // No gravity
            rb.simulated = true;         // Still collides with world
        }

        // 3. Change layer to Echo
        int echoLayer = LayerMask.NameToLayer(echoLayerName);
        if (echoLayer >= 0)
            echo.layer = echoLayer;

        // 4. Optional: Ghost material
        if (echoMaterial != null)
        {
            SpriteRenderer sr = echo.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.material = echoMaterial;
        }
    }
    public float GetTimeRemaining()
    {
        return loopTimer;
    }

}
