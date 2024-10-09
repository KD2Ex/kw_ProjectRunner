using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePickup : InventoryPickup<CreatureInventoryItem>
{
    public override void Pickup(Player player)
    {
        
        gameObject.SetActive(false);
    }
}
