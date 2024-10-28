using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    [SerializeField] private SpeedSign sign;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log($"Progress saved");
        
        SaveSystem.Save(true);
        sign.UpdateSpeed();
    }
}
