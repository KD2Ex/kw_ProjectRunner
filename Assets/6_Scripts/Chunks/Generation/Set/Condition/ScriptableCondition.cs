using UnityEngine;

public abstract class ScriptableCondition : ScriptableObject
{
    public abstract bool Evaluate();
    public abstract void ResetTrigger();
    public abstract ChunkSpawnCondition Init();
}