using System;
using UnityEngine;

[Serializable]
public struct UpgradeCost
{
    [SerializeField] private int cost;
    [SerializeField] private float duration;

    public int Value => cost;
    public float Duration => duration;
}

public class UIBoosterUpgradeSelection : UISelection
{
    [SerializeField] private UpgradeLevel level;
    [SerializeField] private Coins coins;
    [SerializeField] private UpgradeLevelsData data;
    
    [Header("Sounds")]
    [SerializeField] protected AudioSource source;

    public override void Press()
    {
        
        if (level.MaxLevel) return;
        var cost = data.Costs[level.Value + 1];
        
        if (!coins.RemoveCoins(cost.Value)) return;

        if (source.isPlaying) source.Stop();
        source.clip = sounds.GetRandom();
        source.Play();
        
        level.Upgrade();
    }
}
