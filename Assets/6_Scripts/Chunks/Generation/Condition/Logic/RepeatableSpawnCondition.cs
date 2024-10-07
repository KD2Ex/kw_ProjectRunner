using UnityEngine;

public class RepeatableSpawnCondition : ChunkSpawnCondition
{
    private readonly RepeatableTimeCondition data;

    private int TimerValue => (int) data.timerData.Value;
    private int Offset => data.Offset;
    private int Every => data.Every;
    
    private bool triggered;
    private int count = 0;
    private int timeOfTrigger;
    
    public RepeatableSpawnCondition(RepeatableTimeCondition data)
    {
        this.data = data;

        Init();
    }

    public void Init()
    {
        Debug.Log("Repeatable cond init");
        
        triggered = false;
        count = 0;
        timeOfTrigger = 0;
    }

    public override bool Evaluate()
    {
        int timeValue = TimerValue;
        if (timeValue - Offset < 0) return false;

        if (triggered) return true;
        
        int div = timeValue - timeOfTrigger;
        var condition = div >= Every;
        
        if (condition)
        {
            count++;
            triggered = true;
        }
        
        var result = triggered || condition;
        Debug.Log($"rsc result: {result}");
        return result;
    }

    public override void ResetTrigger()
    {
        triggered = false;
        timeOfTrigger = TimerValue;
    }
}