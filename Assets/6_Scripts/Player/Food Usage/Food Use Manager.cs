using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodUseManager : MonoBehaviour
{
    [SerializeField] private Inventory foodInventory;
    
    public void Consume()
    {
        if (foodInventory.Items.Count > 0)
        {
            var index = Random.Range(0, foodInventory.Items.Count);
            var foodItem = foodInventory.Items[index];
            
            var instance = Instantiate(foodItem.UseAnimation, transform);
            instance.SetActive(true);
            
            foodInventory.RemoveItem(foodItem);
        }
    }
}
