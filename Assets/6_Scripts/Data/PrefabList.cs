using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data List", menuName = "Scriptable Objects/Data/List")]
public class PrefabList : ScriptableObject
{
    [SerializeField] private List<GameObject> items;
    public List<GameObject> Items => items;
}
