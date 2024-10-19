using UnityEngine;

public abstract class Setting : ScriptableObject
{
    [SerializeField] protected int maxLevel;
    public abstract void SetLevel(int level);
}