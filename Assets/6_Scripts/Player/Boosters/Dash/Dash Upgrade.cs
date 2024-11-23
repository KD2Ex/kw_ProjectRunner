using System;
using UnityEngine;

public class DashUpgrade : MonoBehaviour
{
    [SerializeField] protected UpgradeLevel m_CurrentLevel;
    [SerializeField] protected UpgradeLevelsData m_LevelsData;
    [SerializeField] protected FloatVariable m_Duration;

    private void Awake()
    {
        GameManager.instance.DashUpgrade = this;
    }

    private void Update()
    {
        m_Duration.Value = m_LevelsData.Costs[m_CurrentLevel.Value].Duration;
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
