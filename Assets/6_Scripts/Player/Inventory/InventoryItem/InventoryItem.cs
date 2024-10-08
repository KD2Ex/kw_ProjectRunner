using UnityEngine;

public abstract class InventoryItem<T> : ScriptableObject
{
    [field: SerializeField] public GameObject UIObject { get; private set; }
    [field: SerializeField] public string Label{ get; private set; }
    [field: SerializeField] public T Item { get; private set; }
}

