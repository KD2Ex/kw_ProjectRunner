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
        [FormerlySerializedAs("CoinsData")] public IntSaveData Coins;
        public IntSaveData Shield;
        public IntSaveData Magnet;
        public IntSaveData X2;
        public IntSaveData Healths;
        public IntSaveData Timer;

        //public LoadedChunkNames Chunks;
        //public ChunkPositionData Position;
    }

    private static string SaveFileName(string name)
    {
        string saveFile = Application.persistentDataPath + $"/{name}" + ".save";
        return saveFile;
    }
    
    public static void Save(bool withRunProgress = false)
    {
        HandleSaveData();

        if (withRunProgress) HandleSaveRun();
        
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
        Debug.Log(saveData.Coins);
        
        GameManager.instance.Coins.Save(ref saveData.Coins);
        GameManager.instance.Player.Shield.Save(ref saveData.Shield);
        GameManager.instance.Player.Magnet.Save(ref saveData.Magnet);
        GameManager.instance.Player.CoinMultiplier.Save(ref saveData.X2);
        GameManager.instance.Player.Save(ref saveData.Healths);
        
    }

    private static void HandleSaveRun()
    {
        GameManager.instance.Timer.Save(ref saveData.Timer);
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
        GameManager.instance.Coins.Load(saveData.Coins);        
        GameManager.instance.Player.Shield.Load(saveData.Shield);
        GameManager.instance.Player.Magnet.Load(saveData.Magnet);
        GameManager.instance.Player.CoinMultiplier.Load(saveData.X2);
        GameManager.instance.Player.Load(saveData.Healths);
        
        GameManager.instance.Timer.Load(saveData.Timer);
        GameManager.instance.UITimer.Load(saveData.Timer);
        GameManager.instance.ChunkSpawnManager.Load(saveData.Timer);
    }

    private static void HandleLoadRun()
    {

        //GameManager.instance.ChunkSpawnManager.ChunksPosition.Load(saveData.Position);
    }
}