using UnityEngine;
using UnityEngine.Events;

public class EventAppearance : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject UIButton;
    
    public UnityEvent OnInteract;

    
    protected float DistanceToPlayer => PlayerLocator.instance.DistanceToPlayer(transform);
    
    private void Accept()
    {
        OnInteract?.Invoke();
    }
    
    private bool start;
    
    void Update()
    {
        if (start) return;
        
        if (DistanceToPlayer < .1f)
        {
            Appear();
            start = true;
        }
    }

    public virtual void Appear()
    {
        input.InteractEvent += Accept;
        UIButton.SetActive(true);
    }

    public virtual void Disappear()
    {
        input.InteractEvent -= Accept;
        Destroy(UIButton.gameObject);
    }
}