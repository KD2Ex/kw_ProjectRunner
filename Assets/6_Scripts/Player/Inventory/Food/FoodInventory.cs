using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Collectable/Food/Inventory")]
public class FoodInventory : Inventory<FoodInventoryItem>
{
    public override bool IsPresent(FoodInventoryItem item)
    {
        Debug.Log(item.Item.Type);
        
        return base.IsPresent(item);
    }
}