using UnityEngine;

public class EventFillCondition : ChunkSpawnCondition
{
    public override bool Evaluate()
    {
        return GameManager.instance.IsEventChunkRunning;
    }

    public override void ResetTrigger()
    {
        
    }
}
