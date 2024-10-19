using UnityEngine;
using Random = UnityEngine.Random;

public class FoodUseManager : MonoBehaviour
{
    [SerializeField] private Inventory foodInventory;

    private bool blocked;
    
    public void Consume()
    {
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
        blocked = true;
    }

    public void UnblockConsuming()
    {
        blocked = false;
    }
}
