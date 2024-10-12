using UnityEngine;

public class DashUpgrade : MonoBehaviour
{
    [SerializeField] protected UpgradeLevel m_CurrentLevel;
    [SerializeField] protected UpgradeLevelsData m_LevelsData;
    [SerializeField] protected FloatVariable m_Duration;

    private void Update()
    {
        m_Duration.Value = m_LevelsData.Costs[m_CurrentLevel.Value].Duration;
    }
}
