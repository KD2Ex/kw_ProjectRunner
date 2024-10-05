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

    public void Initialize()
    {
        Available = true;
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