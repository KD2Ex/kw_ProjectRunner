using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveSystem
{
    private static SaveData saveData;
    private static SettingsSaveData settingsData;

    private const string PlayerProgress = "save";
    private const string Settings = "settings";
    
    [System.Serializable]
    public struct SaveData
    {
        public IntSaveData CoinsData;
        public IntSaveData Shield;
        public IntSaveData Magnet;
        public IntSaveData X2;
        public IntSaveData Healths;

        [FormerlySerializedAs("Chunks")] public LoadedChunkNames Chunks;
        public ChunkPositionData Position;
    }

    private static string SaveFileName(string name)
    {
        string saveFile = Application.persistentDataPath + $"/{name}" + ".save";
        return saveFile;
    }
    
    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(
            SaveFileName(PlayerProgress),
            JsonUtility.ToJson(saveData, true)
            );
        
    }

    public static void SaveSettings()
    {
        HandleSaveSettings();

        
        File.WriteAllText(
            SaveFileName(Settings),
            JsonUtility.ToJson(settingsData, true)
        );
    }

    private static void HandleSaveSettings()
    { 
        GameManager.instance.Config.Save(ref settingsData);
    }

    private static void HandleSaveData()
    {
        // get coins

        Debug.Log(GameManager.instance);
        Debug.Log(GameManager.instance.Coins);
        Debug.Log(saveData);
        Debug.Log(saveData.CoinsData);
        
        GameManager.instance.Coins.Save(ref saveData.CoinsData);
        GameManager.instance.Player.Shield.Save(ref saveData.Shield);
        GameManager.instance.Player.Magnet.Save(ref saveData.Magnet);
        GameManager.instance.Player.CoinMultiplier.Save(ref saveData.X2);
        GameManager.instance.Player.Save(ref saveData.Healths);
        
        GameManager.instance.ChunkSpawnManager.Save(ref saveData.Chunks);
        GameManager.instance.ChunkSpawnManager.ChunksPosition.Save(ref saveData.Position);
    }

    public static void Load()
    {
        if (!File.Exists(SaveFileName(PlayerProgress)))
        {
            Debug.Log(saveData);
            Save();
        }
        
        var saveContent = File.ReadAllText(SaveFileName(PlayerProgress));
        saveData = JsonUtility.FromJson<SaveData>(saveContent);
        
        HandleLoadData();
    }
    
    public static void LoadSettings()
    {
        if (!File.Exists(SaveFileName(Settings)))
        {
            if (settingsData == null)
            {
                settingsData = new SettingsSaveData();
            }
            
            File.WriteAllText(SaveFileName(Settings), JsonUtility.ToJson(settingsData, true));
            Debug.Log("File doesn't exists");
            Debug.Log(settingsData);
        }
        var saveContent = File.ReadAllText(SaveFileName(Settings));
        settingsData = JsonUtility.FromJson<SettingsSaveData>(saveContent);
        
        HandleLoadSettingsData();
    }

    private static void HandleLoadSettingsData()
    {
        GameManager.instance.Config.Load(settingsData);
    }
    
    private static void HandleLoadData()
    {
        GameManager.instance.Coins.Load(saveData.CoinsData);        
        GameManager.instance.Player.Shield.Load(saveData.Shield);
        GameManager.instance.Player.Magnet.Load(saveData.Magnet);
        GameManager.instance.Player.CoinMultiplier.Load(saveData.X2);
        GameManager.instance.Player.Load(saveData.Healths);
        
        GameManager.instance.ChunkSpawnManager.Load(saveData.Chunks);
        GameManager.instance.ChunkSpawnManager.ChunksPosition.Load(saveData.Position);
    }
    
}