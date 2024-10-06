public class ShieldPickup : Collectable
{
    public override void Pickup(Player player)
    {
        player.GetPowerUp(player.Shield);
        gameObject.SetActive(false);
    }
}