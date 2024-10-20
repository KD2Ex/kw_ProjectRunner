using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SettingsConfig Config;
    
    public Player Player { get; private set; }
    public Coins Coins { get; private set; }

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
                Coins = FindObjectOfType<Coins>();
            }
        };
    }
}