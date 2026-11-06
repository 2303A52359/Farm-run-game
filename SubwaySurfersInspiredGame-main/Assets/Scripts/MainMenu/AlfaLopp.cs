using UnityEngine;
using TMPro;

public class AlfaLoop : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed = 1f;

    private TextMeshProUGUI textMeshPro;
    private float timer = 0f;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (textMeshPro == null) return;

        // Use unscaledDeltaTime so animation continues when game is paused
        timer += Time.unscaledDeltaTime * fadeSpeed;

        // Use Mathf.Sin to create smooth fade in/out effect
        // Sin gives values between -1 and 1, so we convert to 0-1 range
        float alpha = (Mathf.Sin(timer) + 1f) / 2f;

        // Update the text alpha
        Color currentColor = textMeshPro.color;
        currentColor.a = alpha;
        textMeshPro.color = currentColor;
    }
}