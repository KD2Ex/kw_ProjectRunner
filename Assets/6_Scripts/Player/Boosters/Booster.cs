using System;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] protected UpgradeLevel m_CurrentLevel;
    [SerializeField] protected UpgradeLevelsData m_LevelsData;
    [SerializeField] protected FloatVariable m_Duration;
    
    protected float elapsedTime = 0f;

    protected virtual void OnEnable()
    {
        // play sound
        // play animation
        m_Duration.Value = m_LevelsData.Costs[m_CurrentLevel.Value].Duration;
        Debug.Log(m_Duration.Value);
    }

    protected virtual void OnDisable()
    {
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > m_Duration.Value)
        {
            gameObject.SetActive(false);
            elapsedTime = 0f;

        }
    }
    
    public void AddDuration()
    {
        elapsedTime -= m_Duration.Value;
    }

    public void Save(ref IntSaveData data)
    {
        data.Value = m_CurrentLevel.Value;
    }

    public void Load(IntSaveData data)
    {
        m_CurrentLevel.SetLevel(data.Value);
    }
    
}

[System.Serializable]
public struct BoosterSaveData
{
    public int Level;
}