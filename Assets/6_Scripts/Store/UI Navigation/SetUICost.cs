using UnityEngine;

public class SetUICost : MonoBehaviour
{
    [SerializeField] private UpgradeLevel level;
    [SerializeField] private FloatVariable currentHealths;
    [SerializeField] private bool useFloat;
    [SerializeField] private UpgradeLevelsData levelsData;
    private DigitsController controller;
    
    void Awake()
    {
        controller = GetComponent<DigitsController>();
    }

    private void Update()
    {
        if (useFloat)
        {
            if ((int) currentHealths.Value == 10)
            {
                controller.SetData(0);
                return;
            }
            
            controller.SetData(levelsData.Costs[(int)currentHealths.Value + 1].Value);
            return;
        }
        
        if (level.MaxLevel)
        {
            controller.SetData(0);
            return;
        }
        controller.SetData(levelsData.Costs[level.Value + 1].Value);
    }
}
