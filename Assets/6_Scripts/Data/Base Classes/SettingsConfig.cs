using UnityEngine;

[CreateAssetMenu(fileName = "Settings Config")]
public class SettingsConfig : ScriptableObject
{
   public SettingsSaveData Data;

   public void Save(ref SettingsSaveData data)
   {
      data = Data;
   }

   public void Load(SettingsSaveData data)
   {
      Data = data;
   }
}

[System.Serializable]
public class SettingsSaveData
{
   [Range(1, 5)]
   public int Brightness;
   [Range(1, 5)]
   public int Master;
   [Range(1, 5)]
   public int SoundFX;
   [Range(1, 5)]
   public int Volume;
   [Range(1, 4)]
   public int Quality;
}