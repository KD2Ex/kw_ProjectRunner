using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Collectable/Food/Inventory")]
public class FoodInventory : Inventory<FoodInventoryItem>
{
    public override bool IsPresent(FoodInventoryItem item)
    {
        return base.IsPresent(item);
    }
}