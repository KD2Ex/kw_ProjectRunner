using System.Collections.Generic;
using UnityEngine;

public abstract class SOListData<T> : ScriptableObject
{
    public List<T> Items;
}