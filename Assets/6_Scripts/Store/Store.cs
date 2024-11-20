using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Store : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [FormerlySerializedAs("StopUIButton")] [SerializeField] private ApproachingButton approachingUIButton;
    [SerializeField] private ApproachingButton InteractUIButton;
    [SerializeField] private AudioSource storeMusic;
    private GameObject Panel;

    public UnityEvent OnStoreApproaching;
    public UnityEvent OnStoreLeave;
    
    private bool canBeOpened;
    private bool approached;

    private void Awake()
    {
        var manager = GameObject.FindGameObjectWithTag("StorePanel").GetComponent<StorePanelManager>();
        Panel = manager.Panel;

        storeMusic.volume = 0f;
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
            storeMusic.Pause();
            InteractUIButton.FadeOut();
            StartCoroutine(WaitForClosing());
        }

        IEnumerator WaitForClosing()
        {
            yield return new WaitUntil(() => !Panel.activeInHierarchy);
            storeMusic.UnPause();
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
        
        SoundFXManager.instance.FadeOut();
        storeMusic.Play();
        StopAllCoroutines();
        StartCoroutine(Coroutines.FadeFloat(0f, 1f, 2f, MusicVolumeSetter));
        
        approached = true;
        OnStoreApproaching?.Invoke();
        approachingUIButton.FadeIn();
    }

    public void OnLeaving()
    {
        OnStoreLeave?.Invoke();
        
        SoundFXManager.instance.FadeIn();
        StopAllCoroutines();
        StartCoroutine(Coroutines.FadeFloat(storeMusic.volume, 0f, 2f, MusicVolumeSetter));
        
        InteractUIButton.FadeOut();
        approachingUIButton.FadeOut();
        canBeOpened = false;
        
        gameObject.SetActive(false);
    }

    private void MusicVolumeSetter(float value)
    {
        storeMusic.volume = value;
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
