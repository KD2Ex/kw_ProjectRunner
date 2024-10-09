using UnityEngine;

public abstract class InventoryPickup<T> : Collectable
{
    [field:SerializeField] public T Data { get; private set; }
}