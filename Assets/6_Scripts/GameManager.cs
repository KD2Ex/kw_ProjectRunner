using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private BrightnessSetting brightnessSetting;
    [SerializeField] private SoundFXSetting soundFXSetting;
    [SerializeField] private MusicSetting musicSetting;
    [SerializeField] private MasterSetting masterSetting;
    
    public SettingsConfig Config;
     
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
}