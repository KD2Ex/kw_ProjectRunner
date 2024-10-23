using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuntimeChunk
{
    public GameObject chunk;
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
public struct ChunkSaveData
{
    public List<string> loadedChunks;
}