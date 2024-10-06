using UnityEngine;
using UnityEngine.Events;

public class Coin : Collectable
{
    public UnityEvent OnPickup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
    }

    private void OnDisable()
    {
        OnPickup?.Invoke();
    }

    public override void Pickup(Player player)
    {
        player.PickupCoin();
        gameObject.SetActive(false);
    }
}
