using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCollectablePickup : MonoBehaviour
{
    [SerializeField] private List<Collectable> collectables;
    [SerializeField] private List<AudioClip> clips;
    
    private void Start()
    {
        var index = Random.Range(0, collectables.Count);

        var inst = Instantiate(collectables[index], transform);
        inst.SetClip(clips[Random.Range(0, clips.Count)]);
    }
}