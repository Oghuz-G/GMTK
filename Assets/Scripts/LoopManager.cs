using UnityEngine;
using System.Collections.Generic;

public class LoopManager : MonoBehaviour
{
    [Header("Loop Settings")]
    public float loopDuration = 5f;
    public int maxEchoCount = 3;
    public bool echoRepeatsMovement = true; // ✅ Tick box in Inspector

    [Header("References")]
    public GameObject playerPrefab;
    public Transform spawnPoint;

    [Header("Echo Settings")]
    public Color echoColor = new Color(1f, 1f, 1f, 0.5f);

    private float loopTimer;
    public int currentEchoCount = 0;
    private bool loopingActive = true;
    private bool finalPlayerSpawned = false;

    private GameObject currentPlayer;
    private List<GameObject> echoes = new List<GameObject>();

    public delegate void LoopEndedHandler();
    public event LoopEndedHandler OnLoopEnded;

    void Start()
    {
        loopTimer = loopDuration;
        SpawnNewPlayer();
    }

    void Update()
    {
        if (!loopingActive) return;

        loopTimer -= Time.deltaTime;

        if (loopTimer <= 0f)
        {
            StartNewLoop();
        }
    }

    private void SpawnNewPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        currentPlayer.name = "Player_Current";
    }

    private void StartNewLoop()
    {
        if (currentPlayer != null)
        {
            GameObject echo = currentPlayer;
            MakeEcho(echo);
            echoes.Add(echo);
            currentEchoCount++;
        }

        // Check if we reached the echo limit
        if (currentEchoCount >= maxEchoCount)
        {
            loopingActive = false;

            // ✅ Invoke the event here to hide the timer UI
            OnLoopEnded?.Invoke();

            if (!finalPlayerSpawned)
            {
                SpawnNewPlayer();
                finalPlayerSpawned = true;
            }

            return; // Exit without starting new loop timer
        }

        loopTimer = loopDuration;
        SpawnNewPlayer();
    }


    private void MakeEcho(GameObject obj)
    {
        int echoLayer = LayerMask.NameToLayer("Echo");
        if (echoLayer != -1)
            obj.layer = echoLayer;

        // Disable player control
        var playerController = obj.GetComponent<PlayerController2D>();
        if (playerController != null)
            playerController.enabled = false;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;

            if (echoRepeatsMovement)
            {
                rb.isKinematic = false; // Let it move via playback
                EchoPlayback playback = obj.AddComponent<EchoPlayback>(); // ✅ Attach playback
                playback.InitializeFromRecorder(obj.GetComponent<PlayerRecorder>());
            }
            else
            {
                rb.isKinematic = true; // Freeze if no replay
            }
        }

        // Apply echo color to sprites
        SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            sr.color = echoColor;
        }
    }

    public float GetTimeRemaining()
    {
        return Mathf.Max(0f, loopTimer);
    }
}
