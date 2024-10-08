using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [field:SerializeField] public FoodInventory Food { get; private set; }
    
    private void Awake()
    {

    }
    
    public bool IsPresent(FoodType food)
    {
        return Food.Items.Find(foodItem => foodItem.Item.Type == food);
    }
}