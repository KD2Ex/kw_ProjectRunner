using UnityEngine;

public class SetUICost : MonoBehaviour
{
    [SerializeField] private UpgradeLevel level;
    [SerializeField] private UpgradeLevelsData levelsData;
    private DigitsController controller;
    
    void Awake()
    {
        controller = GetComponent<DigitsController>();
    }

    private void Update()
    {
        if (level.MaxLevel)
        {
            controller.SetData(0);
            return;
        }
        controller.SetData(levelsData.Costs[level.Value + 1].Value);
    }
}
