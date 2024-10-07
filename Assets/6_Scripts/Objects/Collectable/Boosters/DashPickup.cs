
public class DashPickup : Collectable
{
    public override void Pickup(Player player)
    {
        player.EnterDashState();
        gameObject.SetActive(false);
    }
}
