using _6_Scripts.Utils.DataFormating;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Coins coins;
    [SerializeField] private TMP_Text coinText;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        var intValue = (int) coins.Value;
        coinText.text = DataFormating.FormatIntData(intValue);
    }
}
