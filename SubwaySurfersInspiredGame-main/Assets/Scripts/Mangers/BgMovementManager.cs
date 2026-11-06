using UnityEngine;

public class BgMovemntManager : MonoBehaviour
{
    [Header("Scriptable Object")]
    public ObstacleData obstacleData;

    [Header("Movement Settings")]
    public float resetZPosition = -10f;
    public float restartZPosition = 20f;

    [Header("Assigned Buildings")]
    public GameObject[] Buildings;

    private float currentSpeed;
    private bool isMoving = true;

    void Start()
    {
        currentSpeed = obstacleData.initialSpeed;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveObstacles();
            UpdateSpeed();
        }
    }

    private void MoveObstacles()
    {
        foreach (GameObject Building in Buildings)
        {
            if (Building != null)
            {
                // Move obstacle backward
                Building.transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);

                // Check if obstacle needs to reset
                if (Building.transform.position.z <= resetZPosition)
                {
                    ResetObstacle(Building);
                }
            }
        }
    }

    private void ResetObstacle(GameObject Building)
    {
        Vector3 newPosition = Building.transform.position;
        newPosition.z = restartZPosition;
        Building.transform.position = newPosition;
    }

    private void UpdateSpeed()
    {
        if (currentSpeed < obstacleData.maxSpeed)
        {
            currentSpeed += obstacleData.speedIncreaseRate * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, obstacleData.maxSpeed);
        }
    }

    // Call this function to stop the movement
    public void StopMovement()
    {
        isMoving = false;
    }

    // Call this function to start/resume the movement
    public void StartMovement()
    {
        isMoving = true;
    }

    // Call this function to toggle movement on/off
    public void ToggleMovement()
    {
        isMoving = !isMoving;
    }

    // Call this function to reset speed and start movement
    public void ResetMovement()
    {
        currentSpeed = obstacleData.initialSpeed;
        isMoving = true;
    }
}