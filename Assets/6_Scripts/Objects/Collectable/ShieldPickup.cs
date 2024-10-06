public class ShieldPickup : Collectable
{
    public override void Pickup(Player player)
    {
        player.GetShielded(true);
        gameObject.SetActive(false);
    }
}