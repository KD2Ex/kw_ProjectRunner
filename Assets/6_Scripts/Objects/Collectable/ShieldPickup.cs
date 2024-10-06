public class ShieldPickup : Collectable
{
    public override void Pickup(Player player)
    {
        player.GetShield();
        gameObject.SetActive(false);
    }
}