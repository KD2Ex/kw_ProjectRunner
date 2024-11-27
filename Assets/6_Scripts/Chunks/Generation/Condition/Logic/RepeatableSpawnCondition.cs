using UnityEngine;

public class RepeatableSpawnCondition : ChunkSpawnCondition
{
    private readonly RepeatableTimeCondition data;

    private int TimerValue => (int) data.timerData.Value;
    //private int TimerValue { get; set; }
    private int Offset => data.Offset;
    private int Every => data.Every;
    
    private bool triggered;
    private int timeOfTrigger = 0;
    
    public RepeatableSpawnCondition(RepeatableTimeCondition data)
    {
        this.data = data;
        Init();
    }

    public void Init()
    {
        //Debug.Log("Repeatable cond init");
        
        triggered = false;
        var elapsedFromSave = (int) GameManager.instance.Timer.m_ElapsedTime;
        timeOfTrigger = elapsedFromSave / Every * Every;
        Debug.Log($"Init of {data.name} " + timeOfTrigger);
    }

    public override bool Evaluate()
    {
        int timeValue = TimerValue;
        Debug.Log($"time {timeValue}, offset {Offset}, {timeValue - Offset}");
        
        if (timeValue - Offset < 0) return false;

        //Debug.Log($"{triggered}");
        
        if (triggered) return true;
        
        int div = timeValue - timeOfTrigger;
        var condition = div >= Every;

        Debug.Log($"timeOfTrigger {timeOfTrigger}");
        //Debug.Log($"div {div}");
        
        if (condition)
        {
            triggered = true;
        }
        
        var result = triggered || condition;
        //Debug.Log($"rsc result: {result}");
        return result;
    }

    public override void ResetTrigger()
    {
        Debug.Log("Reset trigger");
        
        triggered = false;
        timeOfTrigger = TimerValue;
        Debug.Log($"time of trigger in Reset: {timeOfTrigger}");
        Debug.Log($"timer value: {TimerValue}");
        Debug.Log($"name: {data.name}");
    }
}