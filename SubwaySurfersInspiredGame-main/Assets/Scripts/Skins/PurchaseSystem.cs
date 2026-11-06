using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
    public CoinData coinData;
    public int price = 100;
    public GameObject lockedButton;
    public Button unlockedButton;

    private Button buttonComponent;
    private void Start()
    {
        buttonComponent = GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        Debug.Log("WOrked");
        // Check if player has enough coins
        if (coinData.coinCount >= price)
        {
            // Subtract price
            coinData.coinCount -= price;

            // Disable locked button
            if (lockedButton != null)
            {
                lockedButton.gameObject.SetActive(false);
            }

            // Enable unlocked button
            if (unlockedButton != null)
            {
                unlockedButton.gameObject.SetActive(true);
            }
        }
    }
}