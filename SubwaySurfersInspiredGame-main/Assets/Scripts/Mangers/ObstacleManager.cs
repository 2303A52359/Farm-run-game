using UnityEngine;

public class ObstacleManager: MonoBehaviour
{
    [Header("Obstacle Spawner Reference")]
    public ObstacleSpawner obstacleSpawner;

    // Optional: Function to stop both loop and movement
    public void StopObstacleLoopAndMovement()
    {
        if (obstacleSpawner != null)
        {
            obstacleSpawner.StopLoop();
            obstacleSpawner.StopMovement();
            Debug.Log("Obstacle loop and movement stopped via animation event");
        }
        else
        {
            Debug.LogWarning("ObstacleSpawner reference is null!");
        }
    }
}
