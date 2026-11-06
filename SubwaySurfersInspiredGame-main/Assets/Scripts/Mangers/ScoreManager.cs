using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public TextMeshProUGUI scoreText;
    public float IncreaseRate = 1f;
    public float PointsPerSecond = 1f; // Points per second
    public float multiplier = 1f;


    private int currentScore = 0;
    private bool isIncreasing = true;
    private Coroutine scoreCoroutine;

    void Start()
    {
        // Start increasing score automatically
        StartScoreIncrease();
    }

    public void StartScoreIncrease()
    {
        if (scoreCoroutine != null)
            StopCoroutine(scoreCoroutine);

        isIncreasing = true;
        scoreCoroutine = StartCoroutine(IncreaseScore());
    }

    public void StopScoreIncrease()
    {
        isIncreasing = false;
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
            scoreCoroutine = null;
        }
    }

    private IEnumerator IncreaseScore()
    {
        while (isIncreasing)
        {
            // Add score based on rate and multiplier
            currentScore += Mathf.RoundToInt(PointsPerSecond * multiplier);

            // Update UI with formatted number
            UpdateScoreText();

            // Wait for one second
            yield return new WaitForSeconds(IncreaseRate);
        }
    }

    private void UpdateScoreText()
    {
        // Format the number to 9 digits with leading zeros
        scoreText.text = currentScore.ToString("000000000");
    }

    // Optional: Call this to manually add points
    public void AddPoints(int points)
    {
        currentScore += points;
        UpdateScoreText();
    }

    // Optional: Set multiplier
    public void SetMultiplier(float newMultiplier)
    {
        multiplier = newMultiplier;
    }

    // Optional: Reset score
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
    }
}