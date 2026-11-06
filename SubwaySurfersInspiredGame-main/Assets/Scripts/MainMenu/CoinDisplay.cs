using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    public CoinData coinData;
    public TMP_Text coinText;
    public string prefix = "Coins: ";

    void Start()
    {
        UpdateCoinDisplay();
    }

    void Update()
    {
        UpdateCoinDisplay();
    }

    void UpdateCoinDisplay()
    {
        if (coinText != null && coinData != null)
        {
            coinText.text = prefix + coinData.coinCount.ToString();
        }
    }
}