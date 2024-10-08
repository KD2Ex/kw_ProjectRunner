using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : Collectable
{
    [SerializeField] private List<AudioClip> clips;
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
        var index = Random.Range(0, clips.Count);
        //SoundFXManager.instance.PlayCoinSound(clips[index], 1f);
        SoundFXManager.instance.PlayClipAtPoint(clips[index], transform, 1f);
        player.PickupCoin(1);
        gameObject.SetActive(false);
    }
    
}
