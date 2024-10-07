using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Repeatable Time Condition", menuName = "Scriptable Objects/Chunks/Conditions/Time Repeatable")]
public class RepeatableTimeCondition : ScriptableCondition
{
    [field: SerializeField] public int Offset {get; private set; }
    [field: SerializeField] public int Every {get; private set; }
    [field: SerializeField] public FloatVariable timerData {get; private set; }
    
    private bool triggered;
    private int count = 0;
    private int timeOfTrigger;
    
    private void OnEnable()
    {
        triggered = false;
        count = 0;
        timeOfTrigger = 0;
    }

    public override bool Evaluate()
    {
        /*int timeValue = (int)timerData.Value;
        if (timeValue - m_Offset < 0) return false;

        if (triggered) return true;
        
        int div = timeValue - timeOfTrigger;
        var condition = div >= m_Every;
        
        if (condition)
        {
            count++;
            triggered = true;
        }
        
        var result = triggered || condition;
        Debug.Log($"div {div}");
        Debug.Log($"Count {count}");
        Debug.Log($"Condition Result {result}");
        return result;*/
        return false;
    }

    public override void ResetTrigger()
    {
        triggered = false;
        timeOfTrigger = (int) timerData.Value;
    }

    public override ChunkSpawnCondition Init()
    {
        return new RepeatableSpawnCondition(this);
    }
}