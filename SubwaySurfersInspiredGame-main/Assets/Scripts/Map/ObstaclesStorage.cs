using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Game/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    [Header("Obstacle Prefabs")]
    public GameObject[] threeLaneObstacles; 
    public GameObject[] twoLaneObstacles;   

    [Header("Coin Prefab")]
    public GameObject coinPrefab; // Coin prefab to spawn in empty lanes

    [Header("Speed Settings")]
    public float initialSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 15f;

    [Header("Spawn Chance Settings")]
    [Range(0f, 1f)]
    public float emptyLaneChance = 0.3f; // Chance to spawn nothing in a lane
}