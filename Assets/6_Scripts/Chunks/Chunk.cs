using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Chunk
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public List<GameObject> Prefabs { get; private set; }
    [field: SerializeField] public int Weight { get; private set; }
    [Tooltip("Chunk will not be spawning again after it's first appearance")]
    [field: SerializeField] public bool BecomeUnavailableAfterSpawn { get; private set; }
    public bool Available = true;

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
}