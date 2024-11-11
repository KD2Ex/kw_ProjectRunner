using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Video;

[System.Serializable]
public class VideoCutscene
{
    public VideoPlayer Vod;
    public bool AutoPlayNext;

    public void Prepare()
    {
        Vod.Prepare();
    }

    public void Play()
    {
        Vod.Play();
        
    }
}

public class CutScene : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [FormerlySerializedAs("vods")] [SerializeField] private VideoCutscene[] videos;

    private int index = 0;
    private VideoPlayer playing;

    private Music locationTheme => GameManager.instance.SceneMusic;
    private bool canPlay;

    public UnityEvent OnEnd;
    
    private void Release() => GameManager.instance.CutsceneRawImage.ReleaseVideoRenderText();
    
    private void Awake()
    {
        foreach (var vod in videos)
        {
            vod.Prepare();
            if (vod.AutoPlayNext)
            {
                vod.Vod.loopPointReached += (video) =>
                {
                    Play();
                };
            }
        }
    }

    private void Start()
    {
        Release();
    }

    private void OnEnable()
    {
        input.CutsceneSkipEvent += Play;
    }

    private void OnDisable()
    {
        input.CutsceneSkipEvent -= Play;
    }

    private void Update()
    {
        canPlay = GameManager.instance.MovementSpeed.Value < .0001f;
    }

    public void Play()
    {
        if (!canPlay) return;
        
        Debug.Log("Play");
        
        if (playing)
        {
            playing.Stop();
        }
        
        if (index == 0)
        {
            EnterCutscene();
        }
        
        if (index == videos.Length)
        {
            ExitCutscene();
            return;
        }
        
        videos[index].Play();
        playing = videos[index].Vod;
        
        index++;
    }

    private void EnterCutscene()
    {
        input.DisableGameplayInput();
        input.DisableUIInput();
        
        Time.timeScale = 0f;
        locationTheme.Source.Pause();
    }

    private void ExitCutscene()
    {
        input.EnableGameplayInput();
        input.EnableUIInput();
        
        locationTheme.Source.UnPause();
        index = 0;

        playing = null;
        Release();
        
        OnEnd?.Invoke();
        
        Time.timeScale = 1f;
    }
}

