using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomBoosterPickup : RandomPickup<Collectable>
{
    protected override GameObject GetRandomPickup(Func<List<Collectable>, List<Collectable>> filter)
    {
        var index = Random.Range(0, m_Pickups.Count);

        return m_Pickups[index].gameObject;
    }

    protected override List<Collectable> Filter(List<Collectable> list)
    {
        return list;


    }
}