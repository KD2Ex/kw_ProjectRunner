using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [SerializeField] private StoreApproachingButton StopUIButton;
    [SerializeField] private StoreApproachingButton InteractUIButton;
    [SerializeField] private GameObject Panel;

    public UnityEvent OnStoreApproaching;
    public UnityEvent OnStoreLeave;
    
    private bool canBeOpened;
    
    private void OnEnable()
    {
        input.InteractEvent += TogglePanel;
    }

    private void OnDisable()
    {
        input.InteractEvent -= TogglePanel;
    }

    private void TogglePanel()
    {
        if (!canBeOpened) return;
        Panel.SetActive(!Panel.activeInHierarchy);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        OnStoreApproaching?.Invoke();
        
        StopUIButton.FadeIn();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        OnStoreLeave?.Invoke();
        
        InteractUIButton.FadeOut();
        canBeOpened = false;
    }

    public void Open()
    {
        canBeOpened = true;
        InteractUIButton.FadeIn();
        StopUIButton.FadeOut();
    }

    public void Close()
    {
        canBeOpened = false;
        InteractUIButton.FadeOut();
        StopUIButton.FadeIn();
    }
}
