using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Booster levels data", menuName = "Scriptable Objects/Boosters/Levels Data")]
public class UpgradeLevelsData : ScriptableObject
{
    [field:SerializeField] public List<UpgradeCost> Costs { get; private set; }
}
