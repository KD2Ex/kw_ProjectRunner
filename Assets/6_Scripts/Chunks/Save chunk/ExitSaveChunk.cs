using UnityEngine;

public class ExitSaveChunk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.instance.Player.FoodUseManager.UnblockConsuming();
    }
}