using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCollectablePickup : MonoBehaviour
{
    [SerializeField] private List<Collectable> collectables;

    private void Start()
    {
        var index = Random.Range(0, collectables.Count);

        Instantiate(collectables[index], transform);
    }
}