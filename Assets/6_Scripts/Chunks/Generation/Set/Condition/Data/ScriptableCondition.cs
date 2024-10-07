using UnityEngine;

public abstract class ScriptableCondition : ScriptableObject
{
    public abstract ChunkSpawnCondition Init();
}