using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BrightnessSetting : Setting
{
    [SerializeField] private PostProcessProfile brightness;
    
    private AutoExposure exposure;
    
    private void Awake()
    {
        brightness.TryGetSettings(out exposure);
    }

    public override void LoadLevelValue(ref int value)
    {
        value = config.Data.Brightness;
        SetLevel(value);
    }

    public override void SetLevel(int level)
    {
        Debug.Log(exposure);
        
        exposure.keyValue.value = GetBrightnessValue(level);
        config.Data.Brightness = level;

        Debug.Log("Setting class  " + level);
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
}