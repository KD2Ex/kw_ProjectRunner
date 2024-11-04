using UnityEngine;
using UnityEngine.Serialization;

public class BossChunkEndTrigger : MonoBehaviour
{
    [FormerlySerializedAs("bossAppearance")] [SerializeField] private EventAppearance eventAppearance;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        eventAppearance.Disappear();
    }
}