using System;
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

    private AudioSource LocationTheme => GameManager.instance.SceneMusic.Source;
    
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

        if (!eventMusic)
        {
            StartCoroutine(FadeMusic(
                LocationTheme,
                () => LocationTheme.volume > .01f,
                value => value - Time.deltaTime));
            return;
        }
        StartCoroutine(ChangeMusicClip(LocationTheme, eventMusic));
    }

    public virtual void Disappear()
    {
        input.InteractEvent -= Accept;
        Destroy(UIButton.gameObject);
        
        if (!eventMusic)
        {
            StartCoroutine(FadeMusic(
                LocationTheme,
                () => LocationTheme.volume < 1f,
                value => value + Time.deltaTime));
            return;
        }
        
        StartCoroutine(ChangeMusicClip(eventMusic, LocationTheme));
    }

    private IEnumerator ChangeMusicClip(AudioSource from, AudioSource to = null)
    {
        while (from.volume > .1f)
        {
            from.volume -= Time.deltaTime;
            yield return null;
        }

        from.Pause();

        if (!to) yield break;
        
        to.Play();
        while (to.volume < 1f)
        {
            to.volume += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeMusic(AudioSource musicSource, Func<bool> predicate, Func<float, float> op)
    {
        while (predicate.Invoke())
        {
            musicSource.volume = op(musicSource.volume);
            yield return null;
        }
    }
}