using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade level", menuName = "Scriptable Objects/Boosters/Upgrade level")]
public class UpgradeLevel : ScriptableObject
{
    [Range(0, 5)]
    [SerializeField] private int value;

    public int Value => value;

    public void Upgrade()
    {
        value++;
    }
}