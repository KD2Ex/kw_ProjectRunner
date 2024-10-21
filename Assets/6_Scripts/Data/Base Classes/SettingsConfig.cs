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
      Debug.Log(Data.Brightness);
   }

}

[System.Serializable]
public class SettingsSaveData
{
   [Range(1, 5)] 
   public int Brightness = 3;
   [Range(1, 5)]
   public int Master = 3;
   [Range(1, 5)]
   public int SoundFX = 3;
   [Range(1, 5)]
   public int Music = 1;
   [Range(1, 4)]
   public int Quality = 4;
}