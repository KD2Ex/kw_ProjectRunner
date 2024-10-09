using System;
using UnityEngine;

public class InventoryCollectable : Collectable
{
    [field:SerializeField] public InventoryItem Item { get; private set; }
    [field:SerializeField] public Inventory Inventory { get; private set; }

    public override void Pickup(Player player)
    {
        Inventory.AddItem(Item);
        gameObject.SetActive(false);
    }
}