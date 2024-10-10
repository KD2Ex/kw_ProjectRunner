using UnityEngine;

public class StoreExitTrigger : MonoBehaviour
{
    private Store store;

    private void Awake()
    {
        store = GetComponentInParent<Store>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        store.OnLeaving();
    }
}
