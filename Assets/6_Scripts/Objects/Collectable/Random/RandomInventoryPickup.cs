using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomInventoryPickup<T> : RandomPickup<InventoryPickup<T>>
{
    [SerializeField] private Inventory<T> Inventory; // replace with condition

    protected override GameObject GetRandomPickup(Func<List<InventoryPickup<T>>, List<InventoryPickup<T>>> filter)
    {
        var list = filter(m_Pickups);
        var index = Random.Range(0, list.Count);

        return list[index].gameObject;
    }

    protected override List<InventoryPickup<T>> Filter(List<InventoryPickup<T>> list)
    {
        List<InventoryPickup<T>> pickups = new();

        foreach (var pickup in list)
        {
            //replace with so condition
            
            var isPresent = Inventory.IsPresent(pickup.Data);
            if (!isPresent) pickups.Add(pickup);
        }
        
        return pickups;
    }
}

