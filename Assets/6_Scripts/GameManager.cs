using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject loadingScreen;
    
    public Player Player { get; private set; }
    public Coins Coins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        SaveSystem.LoadSettings();
        
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Main")
            {
                Player = FindObjectOfType<Player>();
            }
        };
    }

    private void Start()
    {
        brightnessSetting.SetLevel(Config.Data.Brightness);
        soundFXSetting.SetLevel(Config.Data.SoundFX);
        musicSetting.SetLevel(Config.Data.Music);
        masterSetting.SetLevel(Config.Data.Master);
    }

    public void OpenLoadingScreen(AsyncOperation async, Action before = null, Action after = null)
    {
        StartCoroutine(Loading(async, before, after));
    }
    
    private IEnumerator Loading(AsyncOperation async, Action before = null, Action after = null)
    {
        var source = SoundFXManager.instance.PlayLoopedSound(loadingSound.GetRandom(), GameManager.instance.transform, 1f);

        
        before?.Invoke();
        instance.loadingScreen.SetActive(true);
        
        if (after != null)
        {
            async.completed += (asyncOp) => after.Invoke();
        }
        async.completed += (asyncOp) => instance.loadingScreen.SetActive(false);
        
        while (!async.isDone)
        {
            Debug.Log(Mathf.Clamp01(async.progress / .9f));
            yield return null;
        }
        
        Destroy(source.gameObject);
    }
}