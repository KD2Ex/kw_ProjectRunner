using System.Diagnostics.Tracing;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    private AudioSource source;
    private AudioClip originClip;

    public AudioSource Source => source;
    
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        originClip = source.clip;
        GameManager.instance.SceneMusic = this;
    }

    public void SetVolume(float value)
    {
        source.volume = Mathf.Clamp01(value);
    }

    public void SetClip(AudioClip clip)
    {
        source.clip = clip;
    }

    public void RevertClip()
    {
        source.clip = originClip;
    }
    
}
