using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void SetMasterVolume(float level)
    {
        mixer.SetFloat("masterLevel", Mathf.Log10(level) * 20);
    }
    public void SetSoundFXVolume(float level)
    {
        //mixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20);
        mixer.SetFloat("soundFXVolume", level);
    }
    public void SetMusicVolume(float level)
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(level) * 20);
    }
}