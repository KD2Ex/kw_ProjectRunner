using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomInventoryPickup : MonoBehaviour
{
    [SerializeField] private List<InventoryCollectable> Pickups;
    [SerializeField] private Inventory Inventory;
    [SerializeField] private SoundList soundList;
    private void Start()
    {
        var list = GetList();

        if (list.Count == 0) return; // maybe spawn boosters?
        
        var index = Random.Range(0, list.Count);
        var inst = Instantiate(list[index], transform);
        inst.SetClip(soundList.GetRandom());   
    }

    private List<InventoryCollectable> GetList()
    {
        List<InventoryCollectable> result = new();
        
        foreach (var inventoryCollectable in Pickups)
        {
            if (!Inventory.Items.Contains(inventoryCollectable.Item))
            {
                result.Add(inventoryCollectable);
            }
        }

        return result;
    }
}