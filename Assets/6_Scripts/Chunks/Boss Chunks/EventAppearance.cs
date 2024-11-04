using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventAppearance : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject UIButton;

    [SerializeField] private AudioSource eventMusic;
    
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

        StartCoroutine(ChangeMusicClip(GameManager.instance.SceneMusic.Source, eventMusic));
    }

    public virtual void Disappear()
    {
        input.InteractEvent -= Accept;
        Destroy(UIButton.gameObject);
        
        StartCoroutine(ChangeMusicClip(eventMusic, GameManager.instance.SceneMusic.Source));
    }

    private IEnumerator ChangeMusicClip(AudioSource from, AudioSource to)
    {
        while (from.volume > .1f)
        {
            from.volume -= Time.deltaTime;
            yield return null;
        }

        from.Pause();
        
        /*
        if (toOriginal)
        {
            music.RevertClip();
            
            newSource.enabled = false;
            newSource = music.Source;
            music.Source.UnPause();
        }
        else
        {
            music.Source.Pause();
            newSource = music.gameObject.AddComponent<AudioSource>();
            newSource.volume = 0f;
            newSource.clip = clip;


            newSource.Play();
            //music.SetClip(clip);
        }*/

        to.Play();
        while (to.volume < 1f)
        {
            to.volume += Time.deltaTime;
            yield return null;
        }
    }
}