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
    private bool approached;
    
    private void OnEnable()
    {
        input.InteractEvent += OpenPanel;
    }

    private void OnDisable()
    {
        input.InteractEvent -= OpenPanel;
    }
    
    private void OpenPanel()
    {
        if (!Panel.activeInHierarchy)
        {
            Panel.SetActive(true);
            InteractUIButton.FadeOut();
        }
    }
    
    private void TogglePanel()
    {
        if (!canBeOpened) return;

        if (Panel.activeInHierarchy)
        {
            Panel.SetActive(false);
            InteractUIButton.FadeIn();
        }
        else
        {
            Panel.SetActive(true);
            InteractUIButton.FadeOut();
        }
    }
    
    public void OnApproaching()
    {
        if (approached) return;
        
        approached = true;
        OnStoreApproaching?.Invoke();
        
        StopUIButton.FadeIn();
    }

    public void OnLeaving()
    {
        OnStoreLeave?.Invoke();
        
        InteractUIButton.FadeOut();
        StopUIButton.FadeOut();
        canBeOpened = false;
        
        gameObject.SetActive(false);
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
