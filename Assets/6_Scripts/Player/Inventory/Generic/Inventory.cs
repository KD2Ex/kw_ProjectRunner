﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventoryItem> Items;
    public UnityEvent OnAddItem;
    
    public void AddItem(InventoryItem item)
    {
        if (Items.Contains(item)) return;
        Items.Add(item);
        
        OnAddItem?.Invoke();
    }

    public void RemoveItem(InventoryItem item)
    {
        if (!Items.Contains(item)) return;
        Items.Remove(item);
    }

    public void Save(ref InventorySaveData data)
    {
        data.Items = Items;
    }

    public void Load(InventorySaveData data)
    {
        Items = data.Items;
    }
    
}


[System.Serializable]
public struct InventorySaveData
{
    public List<InventoryItem> Items;
}