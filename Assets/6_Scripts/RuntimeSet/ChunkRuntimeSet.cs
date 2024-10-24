using UnityEngine;

[CreateAssetMenu]
public class ChunkRuntimeSet : RuntimeSet<RuntimeChunk>
{
    private void OnEnable()
    {
        Items.Clear();
    }
}