using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade level", menuName = "Scriptable Objects/Boosters/Upgrade level")]
public class UpgradeLevel : ScriptableObject
{
    [Range(0, 10)]
    [SerializeField] private int value;
    
    public int Value => value;

    public void Upgrade()
    {
        if (value == 10) return;
        value++;
    }

    public void SetLevel(int value)
    {
        this.value = value;
    }

    public bool MaxLevel => Value == 10;
}