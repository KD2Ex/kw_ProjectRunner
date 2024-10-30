using System.Collections.Generic;
using UnityEngine;

public interface IChunk
{
    public bool Available { get; set; }
    public int Weight { get; set; }
    public bool BecomeUnavailableAfterSpawn { get; set; }
    public GameObject Prefab { get; set; }
    
    [field: SerializeField] public List<GameObject> Prefabs { get; set; }
    [field: SerializeField] public LinkedChunks Linked { get; set; }
}