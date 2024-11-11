using UnityEngine;

public class CursedCoin : MonoBehaviour
{
    private Screamer screamer => GameManager.instance.ScreamerManager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        screamer.Execute();
        gameObject.SetActive(false);
    }
}