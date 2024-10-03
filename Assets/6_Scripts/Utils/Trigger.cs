using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Trigger : MonoBehaviour
{
    [SerializeField] private bool triggerOnce;

    private bool triggered;
    
    public UnityEvent OnTrigger;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerOnce && triggered) return;
        
        if (!other.CompareTag("Player")) return;
        Debug.Log("Trigger");
        OnTrigger.Invoke();

        if (triggerOnce)
        {
            triggered = true;
        }
        
        //transform.position = new Vector3(transform.position.x + 36f, transform.position.y, 0f);
    }
}
