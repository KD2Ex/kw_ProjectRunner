using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCollectablePickup : MonoBehaviour
{
    [SerializeField] private List<Collectable> collectables;
    [SerializeField] private SoundList soundList;
    
    private void Start()
    {
        var index = Random.Range(0, collectables.Count);
        var inst = Instantiate(collectables[index], transform);
        inst.SetClip(soundList.GetRandom());
    }
}