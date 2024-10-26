using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Settings")]
    
    [SerializeField] private BrightnessSetting brightnessSetting;
    [SerializeField] private SoundFXSetting soundFXSetting;
    [SerializeField] private MusicSetting musicSetting;
    [SerializeField] private MasterSetting masterSetting;
    
    public SettingsConfig Config;

    [Space] [Header("Loading Screen")] [SerializeField]
    private SoundList loadingSound;
    public Animator loadingScreen;
    
    public Player Player { get; set; }
    public ChunkSpawnManager ChunkSpawnManager { get; set; }
    public Coins Coins;
    public Timer Timer;
    public UIDigitsTimer UITimer;
    public CameraShake CameraShake;

    public Music SceneMusic;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        SaveSystem.LoadSettings();
        
        SceneManager.sceneLoaded += (scene, mode) =>
        {

        };
    }

    private void Start()
    {
        
        brightnessSetting.SetLevel(Config.Data.Brightness);
        soundFXSetting.SetLevel(Config.Data.SoundFX);
        musicSetting.SetLevel(Config.Data.Music);
        masterSetting.SetLevel(Config.Data.Master);
    }

    private float lastWidth = 1920;
    private float lastHeight = 1080;
    
    private void Update()
    {
        
    }

    private void Resize()
    {
        Debug.Log($"Height: {Screen.height}, Width: {Screen.width}");
        float width = Screen.width;
        float height = Screen.height;
        float ratio = width / height;
        
        Debug.Log(ratio);
        Debug.Log(16f / 9f);

        if (!Mathf.Approximately(ratio, 16f / 9f))
        {
            Debug.Log("Resolution set");
            Screen.SetResolution((int) lastWidth, (int) lastHeight, false);
        }
        else
        {
            lastWidth = width;
            lastHeight = height;
            Debug.Log($"W {lastWidth} H {lastHeight}");
        }
    }

    public void OpenLoadingScreen(AsyncOperation async, Action before = null, Action after = null)
    {
        StartCoroutine(Loading(async, before, after));
    }
    
    private IEnumerator Loading(AsyncOperation async, Action before = null, Action after = null)
    {
        var source = SoundFXManager.instance.PlayLoopedSound(loadingSound.GetRandom(), GameManager.instance.transform, 1f);
        
        before?.Invoke();
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.Play($"Dev{Random.Range(1, 4)}");
        
        if (after != null)
        {
            async.completed += (asyncOp) => after.Invoke();
        }
        async.completed += (asyncOp) => loadingScreen.gameObject.SetActive(false);
        
        while (!async.isDone)
        {
            Debug.Log(Mathf.Clamp01(async.progress / .9f));
            yield return null;
        }
        
        Destroy(source.gameObject);
    }
}