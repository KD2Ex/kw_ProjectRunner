using TMPro;
using UnityEngine;

public class UIFloatVarable : MonoBehaviour
{
    [SerializeField] private FloatVariable m_Value;

    private TextMeshProUGUI m_Text;

    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        m_Text.text = m_Value.Value.ToString();
    }
}
