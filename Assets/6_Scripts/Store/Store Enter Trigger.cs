using UnityEngine;

public class StoreEnterTrigger : MonoBehaviour
{
    private Store store;

    private void Awake()
    {
        store = GetComponentInParent<Store>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        store.OnApproaching();
    }
}
