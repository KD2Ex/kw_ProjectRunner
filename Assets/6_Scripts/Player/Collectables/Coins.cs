using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private FloatVariable m_Value;
    [SerializeField] private bool m_ResetOnAwake;

    private void Awake()
    {
        if (m_ResetOnAwake) ResetValue();
    }

    public void AddCoins(float value)
    {
        m_Value.Value += value;
    }

    public bool RemoveCoins(float value)
    {
        if (m_Value.Value - value < 0) return false;

        m_Value.Value -= value;
        return true;
    }

    public void ResetValue()
    {
        m_Value.Value = 0f;
    }
}
