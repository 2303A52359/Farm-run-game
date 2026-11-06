using UnityEngine;
using TMPro;

public class Playercollision : MonoBehaviour
{
    [Header("Coin Data")]
    public CoinData coinData; // Reference to your ScriptableObject

    [Header("Map Loop Reference")]
    public MapLoop mapLoop;

    [Header("Animator Reference")]
    public Animator animator;

    [Header("Coin UI")]
    public TextMeshProUGUI coinText;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip coinSound;        // Sound for regular coin
    public AudioClip coin10Sound;      // Sound for 10-coin
    public AudioClip coin50Sound;      // Sound for 50-coin
    public AudioClip obstacleSound;    // Optional: Sound for obstacle collision

    // Local coin variable
    private int coin = 0;

    void Start()
    {
        // If mapLoop reference is not set, try to find it automatically
        if (mapLoop == null)
        {
            mapLoop = FindObjectOfType<MapLoop>();
        }

        // If animator reference is not set, try to get it from this GameObject
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // If still not found, try to find it in children
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        // If audio source is not set, try to get it from this GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // If still not found, try to find it in children or add a new one
        if (audioSource == null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.spatialBlend = 0.5f; // Makes it somewhat 3D
                audioSource.playOnAwake = false;
            }
        }

        // Update coin UI on start
        UpdateCoinUI();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            HandleObstacleCollision();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            HandleObstacleCollision();
        }
        else if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject, 1, coinSound);
        }
        else if (other.CompareTag("Coin10"))
        {
            CollectCoin(other.gameObject, 10, coin10Sound);
        }
        else if (other.CompareTag("Coin50"))
        {
            CollectCoin(other.gameObject, 50, coin50Sound);
        }
    }

    void HandleObstacleCollision()
    {
        // Play obstacle sound if available
        if (obstacleSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(obstacleSound);
        }

        // Stop the map loop when player collides with obstacle
        if (mapLoop != null)
        {
            mapLoop.StopLoop();
        }

        // Trigger the hit animation
        if (animator != null)
        {
            animator.SetTrigger("isHit");
        }
        else
        {
            Debug.LogWarning("Player Animator not found! Make sure to assign the Animator component.");
        }
    }

    void CollectCoin(GameObject coinObject, int value, AudioClip sound)
    {
        // Play coin collection sound
        if (sound != null && audioSource != null)
        {
            audioSource.PlayOneShot(sound);
        }
        else if (audioSource != null)
        {
            // Fallback to default coin sound if specific sound is not set
            if (coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);
            }
            else
            {
                Debug.LogWarning("No coin sound assigned!");
            }
        }
        else
        {
            Debug.LogWarning("AudioSource not found or assigned!");
        }

        // Update local coin variable
        coin += value;

        // Update ScriptableObject to keep it in sync
        if (coinData != null)
        {
            coinData.coinCount += value;
        }
        else
        {
            Debug.LogWarning("CoinData ScriptableObject is not assigned!");
        }

        // Update UI using local variable
        UpdateCoinUI();

        // Destroy the coin object
        Destroy(coinObject);
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            // Use the local coin variable
            coinText.text = coin.ToString();
        }
    }

    // Public method to get current coin count
    public int GetCoinCount()
    {
        return coin;
    }

    // Public method to reset coins
    public void ResetCoins()
    {
        coin = 0;

        // Update ScriptableObject
        if (coinData != null)
        {
            coinData.coinCount = coin;
        }

        UpdateCoinUI();
    }

    // Public method to play a sound (useful for other scripts)
    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}