using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static SaveData saveData;
    
    [System.Serializable]
    public struct SaveData
    {
        public IntSaveData CoinsData;
        public IntSaveData Shield;
        public IntSaveData Magnet;
        public IntSaveData X2;
        public IntSaveData Healths;

        public SettingsSaveData Settings;
    }

    private static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }
    
    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(
            SaveFileName(),
            JsonUtility.ToJson(saveData, true)
            );
        
    }

    public static void SaveSettings()
    {
        HandleSaveSettings();

        File.WriteAllText(
            SaveFileName(),
            JsonUtility.ToJson(saveData, true)
        );
    }

    private static void HandleSaveSettings()
    { 
        GameManager.instance.Config.Save(ref saveData.Settings);
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
    }

    public static void Load()
    {
        var saveContent = File.ReadAllText(SaveFileName());
        saveData = JsonUtility.FromJson<SaveData>(saveContent);
        
        HandleLoadData();
    }
    
    public static void LoadSettings()
    {
        var saveContent = File.ReadAllText(SaveFileName());
        saveData = JsonUtility.FromJson<SaveData>(saveContent);
        
        HandleLoadSettingsData();
    }

    private static void HandleLoadSettingsData()
    {

        GameManager.instance.Config.Load(saveData.Settings);
    }
    
    private static void HandleLoadData()
    {
        GameManager.instance.Coins.Load(saveData.CoinsData);        
        GameManager.instance.Player.Shield.Load(saveData.Shield);
        GameManager.instance.Player.Magnet.Load(saveData.Magnet);
        GameManager.instance.Player.CoinMultiplier.Load(saveData.X2);
        GameManager.instance.Player.Load(saveData.Healths);
        GameManager.instance.Config.Load(saveData.Settings);
    }
    
}