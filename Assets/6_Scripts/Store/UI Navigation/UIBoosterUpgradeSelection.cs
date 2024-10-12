using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBoosterUpgradeSelection : UISelection
{
    [SerializeField] private UpgradeLevel level;

    public override void Press()
    {
        level.Upgrade();
    }
}
