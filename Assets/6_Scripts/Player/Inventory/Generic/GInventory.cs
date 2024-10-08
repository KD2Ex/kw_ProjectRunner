using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory/Inventory")]
public class GInventory : ScriptableObject
{
    public List<GInventoryItem> Items;
    
    public void AddItem(GInventoryItem item)
    {
        if (Items.Contains(item)) return;
        Items.Add(item);
    }

    public void RemoveItem(GInventoryItem item)
    {
        if (!Items.Contains(item)) return;
        Items.Remove(item);
    }
    
}