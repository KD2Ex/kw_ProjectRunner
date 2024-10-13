using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioSource coinFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClipAtPoint(AudioClip clip, Transform spawnPoint, float volume)
    {
        var audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        var clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public AudioSource SpawnSound(AudioClip clip, Transform spawnPoint, float volume)
    {
        var audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;

        return audioSource;
    }

    public AudioSource PlayLoopedSound(AudioClip clip, Transform spawnPoint, float volume)
    {
        var audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();

        return audioSource;
    }

    public void DestroySource(AudioSource source)
    {
        Destroy(source);
    } 
    
    public void PlayCoinSound(AudioClip clip, float volume)
    {
        coinFX.clip = clip;
        coinFX.volume = volume;
        coinFX.Play();
    }
}
