using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private FloatVariable m_Value;
    [SerializeField] private bool m_ResetOnAwake;

    public int Value => (int) m_Value.Value;
    
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

    public void Save(ref IntSaveData data)
    {
        data.Value = Value;
    }

    public void Load(IntSaveData data)
    {
        m_Value.Value = data.Value;
    }
}

[System.Serializable]
public struct IntSaveData
{
    public int Value;
}
