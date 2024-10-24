using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuntimeChunk
{
    public GameObject chunk;
    public float x = 0f;
    public GameObject instance;
    [HideInInspector] public ChunkRuntimeSet runtimeSet;

    public void Initialize()
    {
        runtimeSet.Add(this);
    }

    public void Destroy()
    {
        runtimeSet.Remove(this);
    }
}


[System.Serializable]
public struct LoadedChunkNames
{
    public List<string> Items;
}