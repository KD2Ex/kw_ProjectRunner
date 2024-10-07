using UnityEngine;

[CreateAssetMenu(fileName = "Time Condition", menuName = "Scriptable Objects/Chunks/Conditions/After time")]
public class TimeCondition : ScriptableCondition
{
    [field: SerializeField] public float TimeToEvaluate { get; private set; }
    [field: SerializeField] public FloatVariable timerData { get; private set; }

    public override ChunkSpawnCondition Init()
    {
        return new TimeSpawnCondition(this);
    }

}