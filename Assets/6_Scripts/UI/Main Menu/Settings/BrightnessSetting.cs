using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BrightnessSetting : Setting
{
    [SerializeField] private PostProcessProfile brightness;
    
    private AutoExposure exposure;
    private ColorGrading colorGrading;
    
    private void Awake()
    {
        brightness.TryGetSettings(out exposure);
        brightness.TryGetSettings(out colorGrading);
    }

    public override void LoadLevelValue(ref int value)
    {
        value = config.Data.Brightness;
        SetLevel(value);
    }

    public override void SetLevel(int level)
    {
        //exposure.keyValue.value = GetBrightnessValue(level);
        colorGrading.contrast.value = GetContrastValue(level);
        config.Data.Brightness = level;
    }

    private float GetBrightnessValue(int level)
    {
        switch (level)
        {
            case 1:
                return .2f;
            case 2:
                return .5f;
            case 3:
                return 1f;
            case 4:
                return 2.5f;
            case 5:
                return 4f;
        }

        return 1f;
    }

    private float GetContrastValue(int level)
    {
        switch (level)
        {
            case 1:
                return -30;
            case 2:
                return -15;
            case 3:
                return 0;
            case 4:
                return 15;
            case 5:
                return 30;
        }

        return 1f;
    }
}