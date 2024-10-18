using UnityEngine;

public class RepeatableSpawnCondition : ChunkSpawnCondition
{
    private readonly RepeatableTimeCondition data;

    private int TimerValue => (int) data.timerData.Value;
    //private int TimerValue { get; set; }
    private int Offset => data.Offset;
    private int Every => data.Every;
    
    private bool triggered;
    private int count = 0;
    private int timeOfTrigger = 0;
    
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
        Debug.Log($"time {timeValue}, offset {Offset}, {timeValue - Offset}");
        
        if (timeValue - Offset < 0) return false;

        Debug.Log($"{triggered}");
        
        if (triggered) return true;
        
        int div = timeValue - timeOfTrigger;
        var condition = div >= Every;

        Debug.Log($"timeOfTrigger {timeOfTrigger}");
        Debug.Log($"div {div}");
        
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
        Debug.Log("Reset trigger");
        
        triggered = false;
        timeOfTrigger = TimerValue;
    }
}