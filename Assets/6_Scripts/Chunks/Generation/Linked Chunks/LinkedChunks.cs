using _6_Scripts.Utils.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Linked Chunks", menuName = "Scriptable Objects/Chunks/Linked Chunks")]
public class LinkedChunks : ScriptableObject
{
    public GameObject[] Items;
    [FormerlySerializedAs("Random")] public bool Shuffle;

    public System.Random rand = new System.Random();
    
    public GameObject[] ShuffleItems()
    {
        Items.Shuffle();
        return Items;
    }
}
