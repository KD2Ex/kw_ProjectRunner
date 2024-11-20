using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    
    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioSource coinFX;

    [SerializeField] private AudioMixerGroup mixer;

    private AudioSource sceneMusicSource => GameManager.instance.SceneMusic.Source;
    private float startFXVolume;
    
    private float GetSoundFXVolume()
    {
        mixer.audioMixer.GetFloat("soundFXVolume", out var result);
        return result;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        startFXVolume = GetSoundFXVolume();
        Debug.Log(startFXVolume);
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(Coroutines.FadeFloat(sceneMusicSource.volume, 1f, 1f, VolumeSetter));
        StartCoroutine(Coroutines.FadeFloat(GetSoundFXVolume(), startFXVolume, 30f, MixerSetter));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(Coroutines.FadeFloat(sceneMusicSource.volume, 0f, 1f, VolumeSetter));
        StartCoroutine(Coroutines.FadeFloat(GetSoundFXVolume(), -80f, 30f, MixerSetter));
    }

    private void VolumeSetter(float value)
    {
        sceneMusicSource.volume = value;
    }
    
    private void MixerSetter(float value)
    {
        mixer.audioMixer.SetFloat("soundFXVolume", value);
    }
    
    public void PlayClipAtPoint(AudioClip clip, Transform spawnPoint, float volume)
    {
        /*var audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        var clipLength = audioSource.clip.length;*/
        var audioSource = InstantiateClip(clip, spawnPoint, volume);
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void PlayClipAtPoint(AudioClip clip, Transform spawnPoint, float volume, float time)
    {
        var audioSource = InstantiateClip(clip, spawnPoint, volume);
        Destroy(audioSource.gameObject, time);
    }

    public void PlayClipAsChild(AudioClip clip, Transform spawnPoint, float volume)
    {
        var audioSource = InstantiateAsChild(clip, spawnPoint, volume);
        
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    private AudioSource InstantiateAsChild(AudioClip clip, Transform spawnPoint, float volume)
    {
        var audioSource = Instantiate(soundFXObject, spawnPoint);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        return audioSource;
    }
    
    private AudioSource InstantiateClip(AudioClip clip, Transform spawnPoint, float volume)
    {
        var audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        return audioSource;
    }
    

    public AudioSource SpawnSound(AudioClip clip, Transform spawnPoint, float volume)
    {
        var audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        //audioSource.transform.SetParent(transform);
        return audioSource;
    }

    public AudioSource PlayLoopedSound(AudioClip clip, Transform spawnPoint, float volume)
    {
        var audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity, spawnPoint);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();

        return audioSource;
    }

    public void DestroySource(AudioSource source)
    {
        if (!source) return;
        Destroy(source.gameObject);
    } 
    
    public void PlayCoinSound(AudioClip clip, float volume)
    {
        coinFX.clip = clip;
        coinFX.volume = volume;
        coinFX.Play();
    }
}
