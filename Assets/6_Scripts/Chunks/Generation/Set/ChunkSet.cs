using UnityEngine;

[CreateAssetMenu(fileName = "Chunk Set", menuName = "Scriptable Objects/Chunks/Set")]
public class ChunkSet : ScriptableObject
{
    public ChunkList List;
    public ScriptableCondition SpawnCondition;
    public int Priority;
    public ChunkSpawnCondition Condition;

    public void InitCondition()
    {
        Condition = SpawnCondition.Init();
    }
}