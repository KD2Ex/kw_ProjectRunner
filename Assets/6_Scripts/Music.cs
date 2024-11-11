using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    private static Music instance; 
    
    private AudioSource source;
    private AudioClip originClip;

    public AudioSource Source => source;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameManager.instance.SceneMusic = this;
            DontDestroyOnLoad(this);
            source = GetComponent<AudioSource>();
            originClip = source.clip;
        }
        else
        {
            Destroy(gameObject);
        }
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
