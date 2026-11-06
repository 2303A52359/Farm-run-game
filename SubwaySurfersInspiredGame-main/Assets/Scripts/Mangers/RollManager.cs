using UnityEngine;

public class RollManager : MonoBehaviour
{
    [Header("Player Reference")]
    public GameObject playerObject;

    private BoxCollider playerBoxCollider;
    private Vector3 originalColliderSize;
    private Vector3 originalColliderCenter;
    private bool isRolling = false;

    void Start()
    {
        // If player object is not assigned, try to find it by tag
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }

        // Get the BoxCollider from the player
        if (playerObject != null)
        {
            playerBoxCollider = playerObject.GetComponent<BoxCollider>();
            if (playerBoxCollider == null)
            {
                Debug.LogWarning("No BoxCollider component found on the player object!");
            }
            else
            {
                // Store original collider size and center
                originalColliderSize = playerBoxCollider.size;
                originalColliderCenter = playerBoxCollider.center;
                Debug.Log("Stored original collider size: " + originalColliderSize);
            }
        }
        else
        {
            Debug.LogError("Player object not found! Please assign the player in the Inspector.");
        }
    }

    // Call this to start rolling (reduce collider size)
    public void StartRoll()
    {
        if (isRolling) return;

        isRolling = true;

        if (playerBoxCollider != null)
        {
            playerBoxCollider.size = new Vector3(originalColliderSize.x, originalColliderSize.y * 0.5f, originalColliderSize.z);
            playerBoxCollider.center = new Vector3(originalColliderCenter.x, originalColliderCenter.y * 0.5f, originalColliderCenter.z);
            Debug.Log("Collider reduced for rolling");
        }
    }

    // Call this to end rolling (restore collider size)
    public void EndRoll()
    {
        if (!isRolling) return;

        if (playerBoxCollider != null)
        {
            playerBoxCollider.size = originalColliderSize;
            playerBoxCollider.center = originalColliderCenter;
            Debug.Log("Collider restored to original size");
        }

        isRolling = false;
    }

    // Check if currently rolling
    public bool IsRolling()
    {
        return isRolling;
    }
}