using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource sourcePrefab;
    
    [SerializeField] private AudioClip mainMenuTheme;
    [SerializeField] private AudioClip locationOneTheme;

    private void Awake()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            Debug.Log($"Scene: {scene.name}");
            PlayDependingOn(scene.name);
        };

        var scene = SceneManager.GetActiveScene();
        PlayDependingOn(scene.name);

    }

    private void OnEnable()
    {
        
    }

    private void SetMusic(AudioClip clip)
    {
        Debug.Log(clip.name);

        if (source == null)
        {
            source = Instantiate(sourcePrefab);
        }
        
        source.clip = clip;
        source.Play();
    }

    private void PlayDependingOn(string sceneName)
    {
        switch (sceneName)
        {
            case "Main":
                SetMusic(locationOneTheme);
                break;
            case "Main Menu":
                SetMusic(mainMenuTheme);
                break;
            default:
                source.Pause();
                break;
        }
    }

    public void SetMusicVolume(float value)
    {
        source.volume = Mathf.Clamp01(value);
    }
}
