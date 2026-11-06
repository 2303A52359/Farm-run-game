using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRoll : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public RollManager rollManager;

    [Header("Roll Force Settings")]
    [SerializeField] private float downwardForce = 10f;
    [SerializeField] private ForceMode forceMode = ForceMode.Impulse;

    [Header("Component References")]
    [SerializeField] private Rigidbody rb;

    void Start()
    {
        // Find animator in children if not assigned
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("No Animator component found in children!");
            }
        }

        // Find RollManager if not assigned
        if (rollManager == null)
        {
            rollManager = FindObjectOfType<RollManager>();
            if (rollManager == null)
            {
                Debug.LogError("No RollManager found in scene!");
            }
        }

        // Find Rigidbody if not assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("No Rigidbody component found!");
            }
        }
    }

    // Input System method
    public void OnRoll(InputValue value)
    {
        if (value.isPressed && rollManager != null && !rollManager.IsRolling())
        {
            Roll();
        }
    }

    void Roll()
    {
        // Start roll through RollManager
        if (rollManager != null)
        {
            rollManager.StartRoll();
        }

        // Trigger roll animation
        if (animator != null)
        {
            animator.SetTrigger("isRolling");
        }

        // Apply downward force
        ApplyDownwardForce();
    }

    void ApplyDownwardForce()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.down * downwardForce, forceMode);
        }
    }
}