using UnityEngine;

public class RotateObjectY : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 90f; // Degrees per second

    void Update()
    {
        // Rotate the object around its Y axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}