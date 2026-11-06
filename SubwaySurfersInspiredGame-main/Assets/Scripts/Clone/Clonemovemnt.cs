using UnityEngine;

public class ZAxisMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Movement speed along the Z-axis")]
    public float speed = 5f;

    void Update()
    {
        // Calculate movement amount
        float movement = speed * Time.deltaTime;

        // Move the object along the Z-axis
        transform.Translate(0, 0, movement);
    }
}