using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Sound List", menuName = "Scriptable Objects/Data/Sound List")]
public class SoundList : ScriptableObject
{
    [FormerlySerializedAs("clips")] public List<AudioClip> Clips;

    public AudioClip GetRandom()
    {
        return Clips[Random.Range(0, Clips.Count)];
    }
}
