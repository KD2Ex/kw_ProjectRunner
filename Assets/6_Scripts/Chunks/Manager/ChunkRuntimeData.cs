using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chunk runtime data", menuName = "Scriptable Objects/Chunks/Manager")]
public class ChunkRuntimeData : ScriptableObject
{
    public Chunk CurrentChunk { get; set; }
    [field: SerializeField] public GameObject currentChunkGameObject { get; set; }
    [field: SerializeField] public GameObject prevChunkGameObject { get; set; }
}
