using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Within Time", menuName = "Scriptable Objects/Chunks/Conditions/Within Time")]
public class WithinTimeConditionData : ScriptableCondition
{
    public int Time;
    public int Count;
    public FloatVariable Timer;
    
    public override ChunkSpawnCondition Init()
    {
        return new WithinTimeCondition(this);
    }
}