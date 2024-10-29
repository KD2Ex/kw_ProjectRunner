using UnityEngine;

public class ExitSaveChunk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("unblock");
        GameManager.instance.Player.FoodUseManager.UnblockConsuming();
    }
}