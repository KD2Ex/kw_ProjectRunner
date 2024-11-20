using UnityEngine;
using UnityEngine.Audio;

public class StoreSoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    
    private float GetCurrentSoundFXVolume()
    {
        audioMixerGroup.audioMixer.GetFloat("soundFXVolume", out var result);
        return result;
    }

    void Start()
    {
        var sound = GetCurrentSoundFXVolume();
        audioMixerGroup.audioMixer.SetFloat("storeVolume", sound);
    }

}
