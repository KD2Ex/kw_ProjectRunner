using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CarnavalRuntimeSet : RuntimeSet<CarnavalChunk>
{
    public int maxCount;
    public List<GameObject> backs;

    [HideInInspector] public List<GameObject> originBacks;
    
    private void OnDisable()
    {
        Items.Clear();
    }
}
