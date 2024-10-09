using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item", menuName = "Scriptable Objects/Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public GameObject UIObject;
    public string Label;
    public InventoryItemType Type;
    public GameObject UseAnimation;

    public Inventory Inventory { get; private set; }

    public void SetInventory(Inventory inventory)
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