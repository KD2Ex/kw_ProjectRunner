using UnityEngine;

public class BossChunkEndTrigger : MonoBehaviour
{
    [SerializeField] private BossAppearance bossAppearance;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        bossAppearance.Disappear();
    }
}