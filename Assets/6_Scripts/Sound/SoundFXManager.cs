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
