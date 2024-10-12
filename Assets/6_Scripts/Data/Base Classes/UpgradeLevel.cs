using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade level", menuName = "Scriptable Objects/Boosters/Upgrade level")]
public class UpgradeLevel : ScriptableObject
{
    [Range(0, 5)]
    [SerializeField] private int value;
    

    public int Value => value;

    public void Upgrade()
    {
        if (value == 5) return;
        value++;
    }

    public bool MaxLevel => Value == 10;
}