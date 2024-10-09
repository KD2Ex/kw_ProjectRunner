using System;
using UnityEngine;

[Serializable]
public class Chunk
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public string Label { get; private set; }
    [field: SerializeField] public int Weight { get; private set; }
    [field: SerializeField] public bool RemoveAfterSpawn { get; private set; }
    public bool Available = true;

    [field: SerializeField] public ScriptableCondition ScriptableCondition { get; private set; }

    public ChunkSpawnCondition RestoreCondition { get; private set; }
    
    public void Initialize()
    {
        Available = true;

        if (RemoveAfterSpawn && ScriptableCondition)
        {
            RestoreCondition = ScriptableCondition.Init();
        } 
    }
    
    public void OnInstantiate()
    {
        if (RemoveAfterSpawn)
        {
            Available = false;
        }
    }
    
    public void Restore()
    {
        Available = true;
    }
}