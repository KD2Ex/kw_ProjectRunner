using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Start chunks", menuName = "Scriptable Objects/Chunk set/Start chunks")]
public class StartChunk : ScriptableObject
{
    [field: SerializeField] public List<Chunk> Items { get; private set; }
    public Func<bool> Condition { get; private set; }
    [field: SerializeField] public int Priority { get; private set; }
    
    public void Initialize(Func<bool> condition)
    {
        Condition = condition;
    }
    
    public bool Evaluate()
    {
        return Condition.Invoke();
    }
}