using UnityEngine;
using UnityEngine.InputSystem;

public class SmoothCameraRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private GameObject Camera;

    [Header("Trigger Settings")]
    [SerializeField] private string triggerTag = "Reset";

    [Header("Destruction Settings")]
    [SerializeField] private bool destroyAfterTime = true;
    [SerializeField] private float destroyDelay = 5f; // Time in seconds before destruction
    [SerializeField] private bool startTimerOnTrigger = true; // Start timer when trigger occurs
    [SerializeField] private bool startTimerOnStart = false; // Start timer when object starts

    private bool isRotating = false;
    private float totalRotation = 0f;
    private float targetRotation = 180f;
    private float destroyTimer = 0f;
    private bool timerRunning = false;

    void Start()
    {
        // Start destruction timer on start if enabled
        if (startTimerOnStart && destroyAfterTime)
        {
            StartDestructionTimer();
        }
    }

    void Update()
    {
        // If rotating, apply the rotation
        if (isRotating)
        {
            RotateTransform();
        }

        // Update destruction timer if running
        if (timerRunning)
        {
            UpdateDestructionTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the "Reset" tag
        if (other.CompareTag(triggerTag) && !isRotating)
        {
            StartRotation();
            Debug.Log("Rotation Started");

            // Start destruction timer on trigger if enabled
            if (destroyAfterTime && startTimerOnTrigger)
            {
                StartDestructionTimer();
            }
        }
    }

    private void StartRotation()
    {
        isRotating = true;
        totalRotation = 0f;
    }

    private void RotateTransform()
    {
        // Calculate how much we should rotate this frame
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        // Check if this would exceed our target
        if (totalRotation + rotationThisFrame > targetRotation)
        {
            rotationThisFrame = targetRotation - totalRotation;
            isRotating = false;
        }

        // Add to the current Y rotation (rotates the player)
        Camera.transform.Rotate(0, rotationThisFrame, 0, Space.World);

        // Track total rotation
        totalRotation += rotationThisFrame;

        // If we've reached our target, stop rotating
        if (totalRotation >= targetRotation)
        {
            isRotating = false;
        }
    }

    private void StartDestructionTimer()
    {
        if (destroyAfterTime && destroyDelay > 0)
        {
            timerRunning = true;
            destroyTimer = destroyDelay;
            Debug.Log($"{gameObject.name} will be destroyed in {destroyDelay} seconds");
        }
    }

    private void UpdateDestructionTimer()
    {
        // Count down the timer
        destroyTimer -= Time.deltaTime;

        // Check if timer has reached zero
        if (destroyTimer <= 0f)
        {
            DestroyObject();
            timerRunning = false;
        }
    }

    private void DestroyObject()
    {
        // Destroy the GameObject
        Destroy(gameObject);
        Debug.Log($"{gameObject.name} has been destroyed");
    }
}