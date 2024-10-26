using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    
    [SerializeField] private AudioClip mainMenuTheme;
    [SerializeField] private AudioClip locationOneTheme;

    private void Awake()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            switch (scene.name)
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
        };
    }

    private void OnEnable()
    {
        
    }

    private void SetMusic(AudioClip clip)
    {
        Debug.Log(clip.name);
        
        source.clip = clip;
        source.Play();
    }
}
