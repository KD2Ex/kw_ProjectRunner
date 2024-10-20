using UnityEngine;

public class MusicSetting : Setting
{
    [SerializeField] private SoundMixerManager manager;

    public override void SetLevel(int level)
    {
        manager.SetMusicVolume(GetValue(level));
        config.Data.Music = level;
    }

    public override void LoadLevelValue(ref int value)
    {
        value = config.Data.Music;
        SetLevel(value);
    }
    
    private float GetValue(int level)
    {
        switch (level)
        {
            case 0:
                return -80f;
            case 1:
                return -60f;
            case 2:
                return -40f;
            case 3:
                return -20f;
            case 4:
                return -10f;
            case 5:
                return 0f;
        }

        return 1f;
    }
}
