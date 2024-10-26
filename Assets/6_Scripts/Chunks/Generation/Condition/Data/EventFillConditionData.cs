using UnityEngine;

[CreateAssetMenu(fileName = "Event Fill", menuName = "Scriptable Objects/Chunks/Conditions/Event")]
public class EventFillConditionData : ScriptableCondition
{
    public override ChunkSpawnCondition Init()
    {
        return new EventFillCondition();
    }
}