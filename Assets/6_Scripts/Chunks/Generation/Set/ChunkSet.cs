using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chunk Set", menuName = "Scriptable Objects/Chunks/Set")]
public class ChunkSet : ScriptableObject
{
    public ChunkList List;
    public List<ScriptableCondition> SpawnCondition;
    public int Priority;

    private List<ChunkSpawnCondition> Conditions = new();

    public void InitCondition()
    {
        Conditions.Clear();
        foreach (var condition in SpawnCondition)
        {
            Conditions.Add(condition.Init());
        }
    }

    public bool EvaluateAll()
    {
        var result = true;
        foreach (var condition in Conditions)
        {
            result = result && condition.Evaluate();
        }

        return result;
    }

    public void ResetAll()
    {
        foreach (var condition in Conditions)
        {
            condition.ResetTrigger();
        }
    }
}