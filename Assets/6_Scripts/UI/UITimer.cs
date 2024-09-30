using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TimerData m_Timer;
    private TextMeshProUGUI m_Text;

    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        var text = FormatTime(m_Timer.ElapsedTime);
        m_Text.text = text;
    }

    private string FormatTime(float time)
    {
        var seconds = Mathf.FloorToInt(time);
        var minutes = Mathf.FloorToInt(seconds / 60);
        
        seconds %= 60;
        
        return $"{minutes / 10}{minutes % 10} : {seconds / 10}{seconds % 10}";
    }
}
