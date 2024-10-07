using UnityEngine;

[CreateAssetMenu(fileName = "Counter Condition", menuName = "Scriptable Objects/Chunks/Conditions/After x chunks spawned")]
public class CounterConditionData : ScriptableCondition
{
    [field: SerializeField] public int Count { get; private set; }
    [field: SerializeField] public bool ResetCounterOnSpawn { get; private set; }
    
    public override ChunkSpawnCondition Init()
    {
        return new CounterSpawnCondition(this);
    }
}