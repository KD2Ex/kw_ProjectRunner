using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "Create Setting")]
public class BrightnessSetting : Setting
{
    public override void SetLevel(int level)
    {
        float floatLevel = Convert.ToSingle(level);

        Debug.Log(floatLevel);
        Debug.Log(floatLevel / maxLevel);
        Debug.Log(floatLevel / maxLevel);
        Screen.brightness = floatLevel / maxLevel;

        Debug.Log(Screen.brightness);
    }
}