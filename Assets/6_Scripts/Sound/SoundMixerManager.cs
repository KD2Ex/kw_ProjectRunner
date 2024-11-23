using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundMixerManager : MonoBehaviour
{
    public static SoundMixerManager instance;
    [SerializeField] private AudioMixer mixer;

    public SoundLevelsData levelsData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        levelsData.music = new[]
        {
            -80,
            -60,
            -40,
            -20,
            -10,
            0
        };
        
    }

    private void OnEnable()
    {
        //UnityEngine.Debug.Log("Eanbled " + gameObject.name);
    }
    
    

    public void SetMasterVolume(float level)
    {
        //mixer.SetFloat("masterLevel", Mathf.Log10(level) * 20);
        mixer.SetFloat("masterVolume", level);
    }
    public void SetSoundFXVolume(float level)
    {
        //mixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20);
        mixer.SetFloat("soundFXVolume", level);
    }
    public void SetMusicVolume(float level)
    {
        //mixer.SetFloat("musicVolume", Mathf.Log10(level) * 20);
        mixer.SetFloat("musicVolume", level);
    }

    public void VolumeDownMusic()
    {
        
    }
    
}