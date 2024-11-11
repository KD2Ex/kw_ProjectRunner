using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    private static Music instance;

    [SerializeField] private FloatVariable time;
    
    private AudioSource source;
    private AudioClip originClip;

    private string sceneName;
    
    public AudioSource Source => source;
    
    private void Awake()
    {
        GameManager.instance.SceneMusic = this;
        source = GetComponent<AudioSource>();
        originClip = source.clip;
        
        return;
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        /*SceneManager.sceneLoaded += SceneLoaded;
        source.time = time.Value;*/
    }

    private void OnDisable()
    {
        /*
        SceneManager.sceneLoaded -= SceneLoaded;
        Debug.Log(source.time);
        */
        
    }

    private void Update()
    {
        //time.Value = source.time;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //if (scene)
        Debug.Log($"Music {scene.name}");
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
