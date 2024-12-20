using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Coins Wallet")]
public class Coins : ScriptableObject
{
    [SerializeField] private FloatVariable m_Value;

    public int RunTotal { get; private set; }
    public int Total;/* { get; private set; }*/
    
    public int Value => (int) m_Value.Value;

    public UnityEvent OnAddCoins;
    
    public void AddCoins(float value)
    {
        m_Value.Value += value;
        RunTotal += (int) value;
        Total += (int) value;
        
        OnAddCoins?.Invoke();
    }

    public bool RemoveCoins(float value)
    {
        if (m_Value.Value - value < 0) return false;

        m_Value.Value -= value;
        return true;
    }

    public void ResetRunValue()
    {
        RunTotal = 0;
        Debug.Log("Reset coins value");
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
