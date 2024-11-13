using UnityEngine;
using UnityEngine.Video;

public class VideoClipsData : ScriptableObject
{
    public VideoClip[] Clips;

    public VideoClip GetRandom() => Clips[Random.Range(0, Clips.Length)];
}