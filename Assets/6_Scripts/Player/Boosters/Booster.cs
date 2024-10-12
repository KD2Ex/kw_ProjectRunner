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
}