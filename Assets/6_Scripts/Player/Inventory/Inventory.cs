using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory<T> : ScriptableObject
{
    [field: SerializeField] public List<T> Items { get; private set; }
    
    // logic

    public void AddItem(T item)
    {
        if (Items.Contains(item)) return;
        Items.Add(item);
    }

    public void RemoveItem(T item)
    {
        if (!Items.Contains(item)) return;
        Items.Remove(item);
    }

    public virtual bool IsPresent(T item)
    {
        return Items.Contains(item);
    }
}

