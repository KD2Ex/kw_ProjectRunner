using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    [SerializeField] private SpeedSign sign;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log($"Progress saved");
        // play sound
        
        GameManager.instance.Player.FoodUseManager.BlockConsuming();
        
        SaveSystem.Save(true);
        sign.UpdateSpeed();
    }
}
