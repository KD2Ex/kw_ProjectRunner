using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chunk", menuName = "Scriptable Objects/Chunks/Chunk Data")]
public class ChunkSOData : ScriptableObject, IChunk
{
    [field: SerializeField] public GameObject Prefab { get; set; }
    [field: SerializeField] public List<GameObject> Prefabs { get; set; } = new();
    [field: SerializeField] public LinkedChunks Linked { get; set; }
    [field: SerializeField] public int Weight { get; set; }
    [Tooltip("Chunk will not be spawning again after it's first appearance")]
    [field: SerializeField] public bool BecomeUnavailableAfterSpawn { get; set; }
    
    //public bool Available = true;

    [field: SerializeField] public ScriptableCondition RestoreCondition { get; private set; }

    public ChunkSpawnCondition Condition { get; private set; }
    
    public void Initialize()
    {
        if (BecomeUnavailableAfterSpawn && RestoreCondition)
        {
            Condition = RestoreCondition.Init();
        } 
    }
    
    public void OnInstantiate()
    {
        if (BecomeUnavailableAfterSpawn)
        {
            Available = false;
        }
    }
    
    public void Restore()
    {
        Available = true;
    }

    [field: SerializeField] public bool Available { get; set; } = true;
}