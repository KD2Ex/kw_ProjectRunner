using System;
using UnityEngine;

[Serializable]
public class Chunk
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public string Label { get; private set; }
}