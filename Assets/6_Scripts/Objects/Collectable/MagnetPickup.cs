

public class MagnetPickup : Collectable
{
    public override void Pickup(Player player)
    {
        player.GetMagnet();
        gameObject.SetActive(false);
    }
}
