using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Data")]
    public ObstacleData obstacleData;

    [Header("Spawn Settings")]
    public Vector3 spawnPosition = new Vector3(0f, 0f, 50f);
    public float endPositionZ = -20f;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;

    [Header("Lane Settings")]
    public float laneOffset = 10f; // Distance between lanes

    [Header("Debug")]
    public float currentSpeed;
    public int activeObstaclesCount;
    public bool isMovementStopped = false;

    [Header("Loop Control")]
    public bool isLoopActive = true; // Public variable to control the spawning loop

    private List<GameObject> activeObstacles = new List<GameObject>();
    private float spawnTimer;
    private float nextSpawnTime;
    private bool isSpawningActive = true;
    private float previousSpeed; // Store speed before stopping

    void Start()
    {
        if (obstacleData == null)
        {
            Debug.LogError("ObstacleData is not assigned!");
            return;
        }

        currentSpeed = obstacleData.initialSpeed;
        CalculateNextSpawnTime();
    }

    void Update()
    {
        if (!isSpawningActive || obstacleData == null || !isLoopActive)
            return;

        HandleSpawning();
        HandleObstacleMovement();

        // Only increase speed if movement is not stopped
        if (!isMovementStopped)
        {
            IncreaseSpeedOverTime();
        }

        UpdateDebugInfo();
    }

    void HandleSpawning()
    {
        // Don't spawn new obstacles if movement is stopped or loop is inactive
        if (isMovementStopped || !isLoopActive)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= nextSpawnTime)
        {
            SpawnObstaclePattern();
            spawnTimer = 0f;
            CalculateNextSpawnTime();
        }
    }

    void SpawnObstaclePattern()
    {
        // Define the three lane positions
        Vector3[] lanePositions = {
            new Vector3(-laneOffset, spawnPosition.y, spawnPosition.z), // Left lane
            new Vector3(0f, spawnPosition.y, spawnPosition.z),          // Center lane
            new Vector3(laneOffset, spawnPosition.y, spawnPosition.z)   // Right lane
        };

        // Decide which obstacle pool to use
        GameObject[] selectedObstaclePool = SelectObstaclePool();

        if (selectedObstaclePool == null || selectedObstaclePool.Length == 0)
        {
            Debug.LogWarning("No obstacle prefabs available to spawn!");
            return;
        }

        // For two-lane obstacles, we need to ensure we don't spawn in all three lanes
        bool isTwoLaneObstacle = selectedObstaclePool == obstacleData.twoLaneObstacles;
        List<int> availableLanes = new List<int> { 0, 1, 2 }; // All lanes initially available

        if (isTwoLaneObstacle)
        {
            // Remove one random lane for two-lane obstacles
            int laneToRemove = Random.Range(0, availableLanes.Count);
            availableLanes.RemoveAt(laneToRemove);
        }

        // Track which lanes will have obstacles
        bool[] laneHasObstacle = new bool[3];

        // First pass: Spawn obstacles in available lanes
        foreach (int laneIndex in availableLanes)
        {
            // Check if we should spawn nothing in this lane
            if (Random.value <= obstacleData.emptyLaneChance)
                continue;

            // Select random obstacle from the chosen pool
            int randomIndex = Random.Range(0, selectedObstaclePool.Length);
            GameObject selectedPrefab = selectedObstaclePool[randomIndex];

            if (selectedPrefab != null)
            {
                GameObject newObstacle = Instantiate(selectedPrefab, lanePositions[laneIndex], Quaternion.identity);
                activeObstacles.Add(newObstacle);
                laneHasObstacle[laneIndex] = true;
            }
        }

        // Second pass: Spawn coins in lanes without obstacles
        for (int laneIndex = 0; laneIndex < 3; laneIndex++)
        {
            // Skip lanes that already have obstacles
            if (laneHasObstacle[laneIndex])
                continue;

            // Skip lanes that are not available for two-lane obstacles
            if (isTwoLaneObstacle && !availableLanes.Contains(laneIndex))
                continue;

            // Spawn coin in this empty lane
            if (obstacleData.coinPrefab != null)
            {
                GameObject newCoin = Instantiate(obstacleData.coinPrefab, lanePositions[laneIndex], Quaternion.identity);
                activeObstacles.Add(newCoin);
            }
        }
    }

    GameObject[] SelectObstaclePool()
    {
        // Randomly choose between three-lane and two-lane obstacles
        bool useThreeLane = Random.Range(0, 2) == 0; // 50% chance for each

        if (useThreeLane && obstacleData.threeLaneObstacles.Length > 0)
        {
            return obstacleData.threeLaneObstacles;
        }
        else if (obstacleData.twoLaneObstacles.Length > 0)
        {
            return obstacleData.twoLaneObstacles;
        }
        else if (obstacleData.threeLaneObstacles.Length > 0)
        {
            return obstacleData.threeLaneObstacles; // Fallback to three-lane obstacles
        }

        return null;
    }

    void HandleObstacleMovement()
    {
        // Move all active obstacles and check for deletion
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = activeObstacles[i];

            if (obstacle == null)
            {
                activeObstacles.RemoveAt(i);
                continue;
            }

            // Only move obstacle if movement is not stopped and loop is active
            if (!isMovementStopped && isLoopActive)
            {
                // Move obstacle toward -Z axis
                obstacle.transform.Translate(0f, 0f, -currentSpeed * Time.deltaTime);
            }

            // Check if obstacle should be deleted (still check even when movement is stopped)
            if (obstacle.transform.position.z <= endPositionZ)
            {
                Destroy(obstacle);
                activeObstacles.RemoveAt(i);
            }
        }
    }

    void CalculateNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void IncreaseSpeedOverTime()
    {
        if (currentSpeed < obstacleData.maxSpeed && isLoopActive)
        {
            currentSpeed += obstacleData.speedIncreaseRate * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, obstacleData.maxSpeed);
        }
    }

    void UpdateDebugInfo()
    {
        activeObstaclesCount = activeObstacles.Count;
    }

    // Public methods to control the loop
    public void StopLoop()
    {
        isLoopActive = false;
    }

    public void StartLoop()
    {
        isLoopActive = true;
    }

    // Method to stop all movement
    public void StopMovement()
    {
        isMovementStopped = true;
        previousSpeed = currentSpeed; // Store current speed
        currentSpeed = 0f; // Set speed to zero
    }

    // Method to resume movement
    public void ResumeMovement()
    {
        isMovementStopped = false;
        currentSpeed = previousSpeed; // Restore previous speed
    }

    // Public methods to control spawning
    public void StopSpawning()
    {
        isSpawningActive = false;
    }

    public void StartSpawning()
    {
        isSpawningActive = true;
    }

    public void ResetSpeed()
    {
        currentSpeed = obstacleData.initialSpeed;
        isMovementStopped = false;
    }

    public void ClearAllObstacles()
    {
        foreach (GameObject obstacle in activeObstacles)
        {
            if (obstacle != null)
            {
                Destroy(obstacle);
            }
        }
        activeObstacles.Clear();
    }

    // Clean up when destroyed
    void OnDestroy()
    {
        ClearAllObstacles();
    }
}