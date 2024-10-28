using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        SaveSystem.Save(true);
    }
}
