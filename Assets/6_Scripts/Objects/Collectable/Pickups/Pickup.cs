using System;
using UnityEngine;

public class InventoryCollectable : Collectable
{
    [field:SerializeField] public GInventoryItem Item { get; private set; }
    [field:SerializeField] public GInventory Inventory { get; private set; }

    public override void Pickup(Player player)
    {
        Inventory.AddItem(Item);
        gameObject.SetActive(false);
    }
}