using TMPro;
using UnityEngine;

public class DigitAnimationTest : MonoBehaviour
{
    private int counter = 0;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = counter.ToString();
    }

    private void FixedUpdate()
    {
        counter += 100;
    }
}
