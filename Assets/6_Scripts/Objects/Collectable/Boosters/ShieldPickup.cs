public class ShieldPickup : Collectable
{
    public override void Pickup(Player player)
    {
        player.GetPowerUp(player.Shield);
        
        base.Pickup(player);
    }
}