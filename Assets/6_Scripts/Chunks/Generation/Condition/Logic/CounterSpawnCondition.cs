
public class CounterSpawnCondition : ChunkSpawnCondition
{
    private readonly CounterConditionData data;
    private int counter;

    public CounterSpawnCondition(CounterConditionData data)
    {
        this.data = data;
        counter = 0;
    }

    public override bool Evaluate()
    {
        counter++;
        return counter >= data.Count;
    }

    public override void ResetTrigger()
    {
        if (data.ResetCounterOnSpawn) counter = 0;
    }
}
