using UnityEngine;

public class MapLoop: MonoBehaviour
{
    [Header("Prefabs to Move")]
    public GameObject[] objectsToMove;

    [Header("Movement Settings")]
    public float moveSpeed = 0f;
    public float resetPositionZ = 0f;
    public float endPositionZ = 0f;

    [Header("Player Settings")]
    public GameObject player; // Reference to the player GameObject

    private bool isLoopActive = true;

    void Start()
    {
        // If player reference is not set, try to find it automatically
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        // Only move objects if the loop is active
        if (!isLoopActive || objectsToMove == null || objectsToMove.Length == 0)
            return;

        foreach (GameObject obj in objectsToMove)
        {
            if (obj == null) continue;

            // Move toward -Z
            obj.transform.Translate(0f, 0f, -moveSpeed * Time.deltaTime, Space.World);

            // Loop when passing the end position
            if (obj.transform.position.z <= endPositionZ)
            {
                Vector3 pos = obj.transform.position;
                pos.z = resetPositionZ;
                obj.transform.position = pos;
            }
        }
    }

    // Method to stop the loop
    public void StopLoop()
    {
        isLoopActive = false;
    }

    // Method to start/resume the loop
    public void StartLoop()
    {
        isLoopActive = true;
    }

    // Optional: If you want to handle collision detection in this script
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StopLoop();
        }
    }
}
