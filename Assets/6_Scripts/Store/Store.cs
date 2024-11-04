using System;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Store : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [FormerlySerializedAs("StopUIButton")] [SerializeField] private ApproachingButton approachingUIButton;
    [SerializeField] private ApproachingButton InteractUIButton;
    private GameObject Panel;

    public UnityEvent OnStoreApproaching;
    public UnityEvent OnStoreLeave;
    
    private bool canBeOpened;
    private bool approached;

    private void Awake()
    {
        var manager = GameObject.FindGameObjectWithTag("StorePanel").GetComponent<StorePanelManager>();
        Panel = manager.Panel;

        Debug.Log(Panel.name);
    }

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
        if (!canBeOpened) return;
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

        Debug.Log("Store Approaching");
        
        approached = true;
        OnStoreApproaching?.Invoke();
        approachingUIButton.FadeIn();
    }

    public void OnLeaving()
    {
        OnStoreLeave?.Invoke();
        
        InteractUIButton.FadeOut();
        approachingUIButton.FadeOut();
        canBeOpened = false;
        
        gameObject.SetActive(false);
    }
    
    public void Open()
    {
        canBeOpened = true;
        InteractUIButton.FadeIn();
        approachingUIButton.FadeOut();
    }

    public void Close()
    {
        canBeOpened = false;
        InteractUIButton.FadeOut();
        approachingUIButton.FadeIn();
    }
}
