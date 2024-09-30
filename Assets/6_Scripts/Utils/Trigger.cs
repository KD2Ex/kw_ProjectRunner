using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Trigger : MonoBehaviour
{
    public UnityEvent OnTrigger;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Trigger");
        OnTrigger.Invoke();

        //transform.position = new Vector3(transform.position.x + 36f, transform.position.y, 0f);
    }
}
