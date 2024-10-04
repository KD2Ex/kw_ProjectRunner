using System;
using UnityEngine;

[Serializable]
public class Chunk
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public string Label { get; private set; }
    [field: SerializeField] public int Weight { get; private set; }
    [SerializeField] private bool RemoveAfterSpawn;
    public bool Available = true;
    public ScriptableCondition RestoreCondition;
    
    
    public void Restore()
    {
        Available = true;
    }
}