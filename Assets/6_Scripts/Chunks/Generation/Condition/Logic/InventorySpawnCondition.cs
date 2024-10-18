public class InventorySpawnCondition : ChunkSpawnCondition
{
    private readonly InventoryConditionData data;

    public InventorySpawnCondition(InventoryConditionData data)
    {
        this.data = data;
    }

    public override bool Evaluate()
    {
        return data.inventory.Items.Count != data.fullAmount;
    }

    public override void ResetTrigger()
    {
        
    }
}