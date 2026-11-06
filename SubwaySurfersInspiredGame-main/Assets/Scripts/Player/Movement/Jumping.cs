using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 10f;

    [Header("References")]
    public Animator animator;

    [Header("Roll Manager")]
    public RollManager RollManager;

    private Rigidbody rb;
    

    void Start()
    {
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found on the player! Jumping requires a Rigidbody.");
        }

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


    // Input System method
    public void OnJump(InputValue value)
    {
        if (value.isPressed && animator != null && animator.GetBool("isGrounded"))
        {
            Jump();
            RollManager.EndRoll();
        }
    }

    void Jump()
    {
        // Apply jump force
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Trigger jump animation and set grounded to false
        if (animator != null)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("isGrounded", false);
        }
    }

    // Called when this object collides with another
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetGrounded(true);
        }
    }

    // Optional: Also handle staying on ground for better reliability
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetGrounded(true);
        }
    }

    // Called when this object stops colliding with another
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetGrounded(false);
        }
    }

    private void SetGrounded(bool grounded)
    {
        if (animator != null)
        {
            animator.SetBool("isGrounded", grounded);
        }
    }
}