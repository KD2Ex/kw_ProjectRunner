using UnityEngine;

[CreateAssetMenu(fileName = "Coins Wallet")]
public class Coins : ScriptableObject
{
    [SerializeField] private FloatVariable m_Value;
    [SerializeField] private bool m_ResetOnAwake;

    public int Total { get; private set; }
    
    public int Value => (int) m_Value.Value;
    
    private void Awake()
    {
        if (m_ResetOnAwake) ResetValue();
    }

    public void AddCoins(float value)
    {
        m_Value.Value += value;
        Total += (int) value;
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

    public void Save(ref CoinsSaveData data)
    {
        data.Current = Value;
        data.Total = Total;
    }

    public void Load(CoinsSaveData data)
    {
        m_Value.Value = data.Current;
        Total = data.Total;
    }
}

[System.Serializable]
public struct CoinsSaveData
{
    public int Current;
    public int Total;
}

[System.Serializable]
public struct IntSaveData
{
    public int Value;
}
