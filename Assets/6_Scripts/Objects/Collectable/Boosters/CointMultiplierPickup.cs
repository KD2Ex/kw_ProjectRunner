

public class CointMultiplierPickup : Collectable
{
    public override void Pickup(Player player)
    {
        player.GetPowerUp(player.CoinMultiplier);
        gameObject.SetActive(false);
    }
}
