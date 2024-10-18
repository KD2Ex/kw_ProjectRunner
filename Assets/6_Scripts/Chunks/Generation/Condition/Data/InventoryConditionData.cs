using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Condition", menuName = "Scriptable Objects/Chunks/Conditions/Inventory")]
public class InventoryConditionData : ScriptableCondition
{
    [field: SerializeField] public Inventory inventory;
    [field: SerializeField] public int fullAmount; 
    
    public override ChunkSpawnCondition Init()
    {
        return new InventorySpawnCondition(this);
    }
}