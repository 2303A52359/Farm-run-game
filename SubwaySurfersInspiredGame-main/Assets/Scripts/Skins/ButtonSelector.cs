using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleSkinButton : MonoBehaviour
{
    public SkinData skinData;
    public int buttonIndex;
    public SimpleSkinButton[] allButtons;

    private TMP_Text buttonText;
    private Button buttonComponent;

    void Start()
    {
        // Get components
        buttonText = GetComponentInChildren<TMP_Text>();
        buttonComponent = GetComponent<Button>();

        // Setup onClick listener
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(OnButtonClick);
        }

        // Update button text on start
        UpdateButtonText();
    }

    void OnButtonClick()
    {
        if (skinData != null)
        {
            // Set the skin index
            skinData.skinIndex = buttonIndex;

            // Update all buttons
            UpdateAllButtons();
        }
    }

    void UpdateAllButtons()
    {
        foreach (SimpleSkinButton button in allButtons)
        {
            if (button != null)
            {
                button.UpdateButtonText();
            }
        }
    }

    void UpdateButtonText()
    {
        if (buttonText != null)
        {
            if (skinData != null && skinData.skinIndex == buttonIndex)
            {
                buttonText.text = "Selected";
            }
            else
            {
                buttonText.text = "Select";
            }
        }
    }
}