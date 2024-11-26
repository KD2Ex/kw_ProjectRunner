using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodUseManager : MonoBehaviour
{
    [SerializeField] private Inventory foodInventory;

    private bool _blocked;
    
    private bool blocked
    {
        get => _blocked;
        set
        {
            _blocked = value;
        }
    }

    public void Consume()
    {
        Debug.Log($"Blocked in consume " + blocked);
        Debug.Log(foodInventory.Items.Count);
        
        
        if (foodInventory.Items.Count > 0 && !blocked)
        {
            var index = Random.Range(0, foodInventory.Items.Count);
            var foodItem = foodInventory.Items[index];
            
            var instance = Instantiate(foodItem.UseAnimation, transform);
            instance.SetActive(true);
            
            foodInventory.RemoveItem(foodItem);
        }
    }

    public void BlockConsuming()
    {
        Debug.Log("consuming blocked");
        blocked = true;
        _blocked = true;
    }
    
    public void UnblockConsuming()
    {
        blocked = false;
        Debug.Log("Unblocked");
    }
}
