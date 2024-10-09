
public class FoodPickup : InventoryPickup<FoodInventoryItem>
{
    //[field:SerializeField] public FoodInventoryItem Data { get; private set; }
    
    public override void Pickup(Player player)
    {
        player.PickupFood(Data);
        gameObject.SetActive(false);
    }
}
