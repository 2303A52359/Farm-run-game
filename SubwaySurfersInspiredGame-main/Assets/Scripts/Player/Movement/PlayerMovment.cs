using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment: MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 10f;
    public float smoothTime = 0.2f;

    [Header("Boundaries")]
    public float leftBoundary = -10f;
    public float rightBoundary = 10f;

    [Header("References")]
    public Animator animator;

    [Header("Roll Manager")]
    public RollManager RollManager;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    

    void Start()
    {
        // Initialize target position to current position
        targetPosition = transform.position;

        // Find animator in children if not assigned
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("No Animator component found in children!");
            }
        }
    }

    void Update()
    {
        // Smoothly move towards target position on X axis only
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
    }

    // Input System methods
    public void OnRight(InputValue value)
    {
        if (value.isPressed)
        {
            MoveRight();
            RollManager.EndRoll();  
        }
    }

    public void OnLeft(InputValue value)
    {
        if (value.isPressed)
        {
            MoveLeft();
            RollManager.EndRoll();
        }
    }

    void MoveRight()
    {
        // Calculate new X position
        float newX = targetPosition.x + moveDistance;

        // Check if within right boundary
        if (newX <= rightBoundary)
        {
            // Trigger animation
            if (animator != null)
            {
                animator.SetTrigger("Right");
            }

            targetPosition.x = newX;
        }
        else
        {
            // Clamp to boundary if exceeded
            targetPosition.x = rightBoundary;
        }
    }

    void MoveLeft()
    {
        // Calculate new X position
        float newX = targetPosition.x - moveDistance;

        // Check if within left boundary
        if (newX >= leftBoundary)
        {
            // Trigger animation
            if (animator != null)
            {
                animator.SetTrigger("Left");
            }

            targetPosition.x = newX;
        }
        else
        {
            // Clamp to boundary if exceeded
            targetPosition.x = leftBoundary;
        }
    }
}