using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Video Clips", menuName = "Scriptable Objects/Data/Video clips")]
public class VideoClipsData : ScriptableObject
{
    public VideoClip[] Clips;

    public VideoClip GetRandom() => Clips[Random.Range(0, Clips.Length)];
}