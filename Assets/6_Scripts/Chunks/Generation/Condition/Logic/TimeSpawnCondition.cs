public class TimeSpawnCondition : ChunkSpawnCondition
{
    private readonly TimeCondition data;
    
    public TimeSpawnCondition(TimeCondition data)
    {
        this.data = data;
    }

    public override bool Evaluate() => data.timerData.Value > data.TimeToEvaluate;

    public override void ResetTrigger()
    {
        
    }
}