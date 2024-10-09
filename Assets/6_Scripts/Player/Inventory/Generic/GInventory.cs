using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory/Inventory")]
public class GInventory : ScriptableObject
{
    public List<GInventoryItem> Items;
    public UnityEvent OnAddItem;
    
    public void AddItem(GInventoryItem item)
    {
        if (Items.Contains(item)) return;
        Items.Add(item);
        
        OnAddItem?.Invoke();
    }

    public void RemoveItem(GInventoryItem item)
    {
        if (!Items.Contains(item)) return;
        Items.Remove(item);
    }
}