using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound List", menuName = "Scriptable Objects/Data/Sound List")]
public class SoundList : ScriptableObject
{
    [SerializeField] private List<AudioClip> clips;

    public AudioClip GetRandom()
    {
        return clips[Random.Range(0, clips.Count)];
    }
}
