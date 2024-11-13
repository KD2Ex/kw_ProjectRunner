using TMPro;
using UnityEngine;

public class ScorePanelDigits : MonoBehaviour
{
    [SerializeField] private FloatVariable coins;
    [SerializeField] private TMP_Text text;

    [SerializeField] private int rate;
    
    private void OnEnable()
    {
        for (int i = 0; i < coins.Value; i += rate)
        {
            text.text = i.ToString();
        }

        text.text = coins.Value.ToString();
    }
}