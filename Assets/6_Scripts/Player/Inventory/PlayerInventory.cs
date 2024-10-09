using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [field:SerializeField] public FoodInventory Food { get; private set; }

    public GInventory FoodInv;
    public GInventory CreaturesInv;
    
    private void Awake()
    {

    }
    
    
}