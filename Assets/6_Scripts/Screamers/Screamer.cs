using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Screamer : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [Header("Components")]
    
    [SerializeField] private Animator animator;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource source;
    [SerializeField] private Image Image;
    [SerializeField] private SlowTime slowTime;

    [Header("Data")]
    [SerializeField] private ScreamersClips screamers;


    private const string SOUND_FX_VOLUME = "soundFXVolume";
    private const string SCREAMERS_VOLUME = "screamerVolume";
    
    private void Awake()
    {
        var clips = animator.runtimeAnimatorController.animationClips;
        Debug.Log($"Length: {clips}");
        
        GameManager.instance.ScreamerManager = this;

        Image.enabled = false;
    }

    private void Start()
    {
        mixer.GetFloat(SOUND_FX_VOLUME, out var volume);
        Debug.Log(volume);
        mixer.SetFloat(SCREAMERS_VOLUME, volume);
    }

    private void OnDisable()
    {
        Image.enabled = false;
    }

    public void Execute()
    {
        var screamer = screamers.Clips[Random.Range(0, screamers.Clips.Length)];

        Image.enabled = true;
        
        Image.color = screamer.ImageColor;
        //animator.Play(screamer.AnimClip.name, 0, 0f);
        //SoundFXManager.instance.PlayClipAtPoint(screamer.AudioClip, GameManager.instance.Player.transform, 1f);
        source.clip = screamer.AudioClip;
        source.Play();
        slowTime.StopTime();
        
        //input.DisableGameplayInput();
        input.DisableUIInput();

        Debug.Log(screamer.AnimClip.length);
        
        GameManager.instance.SceneMusic.Source.Pause();

        mixer.GetFloat(SOUND_FX_VOLUME, out var originalVolume);
        mixer.SetFloat(SOUND_FX_VOLUME, -80f);
        StartCoroutine(Coroutines.WaitForUnscaled(screamer.AudioClip.length, AfterScreamer));
        
        void AfterScreamer()
        {
            //input.EnableGameplayInput();
            input.EnableUIInput();
            GameManager.instance.SceneMusic.Source.UnPause();
            slowTime.RevertTime();

            mixer.SetFloat(SOUND_FX_VOLUME, originalVolume);
            Image.enabled = false;
        }
    }
}
