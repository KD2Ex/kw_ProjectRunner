using UnityEngine;

public abstract class Setting : MonoBehaviour
{
    [SerializeField] protected SettingsConfig config;
    public abstract void SetLevel(int level);
    public abstract void LoadLevelValue(ref int value);
}