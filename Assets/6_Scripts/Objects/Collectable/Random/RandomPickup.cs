using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class RandomPickup<T> : MonoBehaviour
{
    [SerializeField] protected List<T> m_Pickups;

    private void OnEnable()
    {
        var instance = 
            Instantiate(
                GetRandomPickup(Filter),
                transform);
    }

    protected abstract GameObject GetRandomPickup(Func<List<T>, List<T>> filter);
    protected abstract List<T> Filter(List<T> list);
}

