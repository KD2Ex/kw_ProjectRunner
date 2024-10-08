using UnityEngine;

public enum FoodType
{
    BURGER,
    HOTDOG,
    ONIGIRI,
    PIZZA
}

[CreateAssetMenu(fileName = "Food", menuName = "Scriptable Objects/Collectable/Food/Type")]
public class FoodData : ScriptableObject
{
    [field: SerializeField] public FoodType Type { get; private set; }
}