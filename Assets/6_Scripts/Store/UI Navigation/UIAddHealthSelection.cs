using UnityEngine;

public class UIAddHealthSelection : UISelection
{

    [SerializeField] private Coins coins;
    [SerializeField] private FloatVariable currentHealths;
    [SerializeField] private UpgradeLevelsData data;
    
    public override void Press()
    {
        if ((int)currentHealths.Value == 10) return;

        var cost = data.Costs[(int)currentHealths.Value + 1];
        if (!coins.RemoveCoins(cost.Value)) return;

        currentHealths.Value++;
    }
}