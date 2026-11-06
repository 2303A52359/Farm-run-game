using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementToggle : MonoBehaviour
{
    public PlayerInput playerInput;
    private bool wasEnabled = true;

    void Start()
    {
        EnableMovement();
        Debug.Log("Player movement Enabled");

        if (playerInput == null)
        {
            Debug.LogError("No PlayerInput component found on this GameObject!");
        }
    }

    // Call this function to disable player input
    public void DisableMovement()
    {
        if (playerInput != null && playerInput.enabled)
        {
            playerInput.enabled = false;
            wasEnabled = true;
            Debug.Log("Player input DISABLED");
        }
    }

    // Call this function to enable player input
    public void EnableMovement()
    {
        if (playerInput != null && !playerInput.enabled)
        {
            playerInput.enabled = true;
            wasEnabled = true;
            Debug.Log("Player input ENABLED");
        }
    }

    // Toggle player input on/off
    public void ToggleMovement()
    {
        if (playerInput != null)
        {
            playerInput.enabled = !playerInput.enabled;
            Debug.Log($"Player input: {(playerInput.enabled ? "ENABLED" : "DISABLED")}");
        }
    }

    // Enable movement if it was previously enabled
    public void RestoreMovement()
    {
        if (playerInput != null && wasEnabled)
        {
            playerInput.enabled = true;
            Debug.Log("Player input RESTORED");
        }
    }
}