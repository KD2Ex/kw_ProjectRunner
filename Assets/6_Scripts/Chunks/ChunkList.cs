﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chunk List", menuName = "Scriptable Objects/Chunks/List")]
public class ChunkList : ScriptableObject
{
    public List<ChunkSOData> TestItems;
    public bool RestoreOnEnable;
    
} 