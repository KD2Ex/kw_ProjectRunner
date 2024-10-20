using UnityEngine;

public class SoundFXSetting : Setting
{
    [SerializeField] private SoundMixerManager manager;
    
    public override void SetLevel(int level)
    {
        manager.SetSoundFXVolume(GetValue(level));
        config.Data.SoundFX = level;
    }

    public override void LoadLevelValue(ref int value)
    {
        value = config.Data.SoundFX;
        SetLevel(value);
    }

    /*private float GetValue(int level)
    {
        switch (level)
        {
            case 0:
                return .0001f;
            case 1:
                return .001f;
            case 2:
                return .02f;
            case 3:
                return .1f;
            case 4:
                return .5f;
            case 5:
                return 1f;
        }

        return 1f;
    }*/

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