using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item", menuName = "Scriptable Objects/Inventory/Item")]
public class GInventoryItem : ScriptableObject
{
    public GameObject UIObject;
    public string Label;
    public InventoryItemType Type;

    public GInventory Inventory { get; private set; }

    public void SetInventory(GInventory inventory)
    {
        Inventory = inventory;
    }

    public void AddToInv()
    {
        Inventory.AddItem(this);
    }

    public void RemoveFromInv()
    {
        Inventory.RemoveItem(this);
    }
}