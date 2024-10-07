using System;
using UnityEngine;

public class FindAndRestore : MonoBehaviour
{
    [SerializeField] private GameObject chunk;
    [SerializeField] private ChunkList chunkList;
    [SerializeField] private ChunkRandomManager RandomManager;
    
    public void Restore()
    {
        var list = RandomManager.Sets.Find(set => set.List == chunkList);
        var item = list.List.Items.Find(item => item.Prefab.name == this.chunk.name);
        
        item.Restore();
    }
}
