using UnityEngine;

[CreateAssetMenu(fileName = "Screamer", menuName = "Scriptable Objects/Screamers/Item")]
public class ScreamerData : ScriptableObject
{
    public AnimationClip AnimClip;
    public AudioClip AudioClip; 
    public Color ImageColor;
}