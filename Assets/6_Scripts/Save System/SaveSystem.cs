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
    }

    public static string SaveFileName()
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

    private static void HandleSaveData()
    {
        // get coins
        
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

    private static void HandleLoadData()
    {
        GameManager.instance.Coins.Load(saveData.CoinsData);        
        GameManager.instance.Player.Shield.Load(saveData.Shield);
        GameManager.instance.Player.Magnet.Load(saveData.Magnet);
        GameManager.instance.Player.CoinMultiplier.Load(saveData.X2);
        GameManager.instance.Player.Load(saveData.Healths);
    }
    
}