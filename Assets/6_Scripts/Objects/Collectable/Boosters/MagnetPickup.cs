
public class MagnetPickup : Collectable
{
    public override void Pickup(Player player)
    {
        player.GetPowerUp(player.Magnet);
        gameObject.SetActive(false);
    }
}
