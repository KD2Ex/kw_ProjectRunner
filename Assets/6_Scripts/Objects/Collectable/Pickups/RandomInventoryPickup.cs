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

        Debug.Log(list.Count);
        foreach (var item in list)
        {
            Debug.Log(item.Item.name);
        }
        
        if (list.Count == 0) return; // maybe spawn boosters?
        var index = 0;

        while (list.Count > 0)
        {
            index = Random.Range(0, list.Count);
            
            if (GameManager.instance.Existing.Contains(list[index].Item))
            {
                list.Remove(list[index]);
            }
            else
            {
                var inst = Instantiate(list[index], transform);
                inst.SetClip(soundList.GetRandom());   
                GameManager.instance.Existing.Add(list[index].Item);
                break;
            }
        }
        
        // spawn booster instead
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