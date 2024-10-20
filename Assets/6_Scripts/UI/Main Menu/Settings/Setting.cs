using UnityEngine;

public abstract class Setting : MonoBehaviour
{
    [SerializeField] protected int maxLevel;
    public abstract void SetLevel(int level);
    public abstract void LoadLevelValue(ref int value);
}