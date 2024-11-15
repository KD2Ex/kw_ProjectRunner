using System;
using UnityEngine;
using UnityEngine.Video;

public class ScorePanelOne : ScorePanelElement
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private AudioSource source;
    [SerializeField] private SoundList soundList;
    
    private void Awake()
    {
        source.clip = soundList.GetRandom();
    }

    private void OnEnable()
    { 
        videoPlayer.Prepare();
        videoPlayer.loopPointReached += OnVideoEnd;
    }
    
    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vPlayer) => scorePanel.PlayNext();
    
    public override void Execute()
    {
        videoPlayer.Play();
        source.Play();
    }

    public override void Stop()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
        videoPlayer.Stop();
        GameManager.instance.CutsceneRawImage.ReleaseVideoRenderText();        
    }
}