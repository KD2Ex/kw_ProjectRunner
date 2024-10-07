using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Repeatable Time Condition", menuName = "Scriptable Objects/Chunks/Conditions/Time Repeatable")]
public class RepeatableTimeCondition : ScriptableCondition
{
    [field: SerializeField] public int Offset {get; private set; }
    [field: SerializeField] public int Every {get; private set; }
    [field: SerializeField] public FloatVariable timerData {get; private set; }
    
    public override ChunkSpawnCondition Init()
    {
        return new RepeatableSpawnCondition(this);
    }
    
}