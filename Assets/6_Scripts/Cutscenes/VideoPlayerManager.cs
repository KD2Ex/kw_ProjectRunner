using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    public void Play()
    {
        videoPlayer.Play();
    }
}


#if UNITY_EDITOR

[CustomEditor(typeof(VideoPlayerManager))]
public class VideoPlayerManagerCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        VideoPlayerManager manager = target as VideoPlayerManager;

        if (GUILayout.Button("Play Video"))
        {
            manager.Play();
        }
    }
}

#endif
