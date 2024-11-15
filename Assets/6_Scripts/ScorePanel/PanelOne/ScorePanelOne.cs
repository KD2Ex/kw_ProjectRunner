using UnityEngine;
using UnityEngine.Video;

public class ScorePanelOne : ScorePanelElement
{
    [SerializeField] private VideoPlayer videoPlayer;

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
    }

    public override void Stop()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
        videoPlayer.Stop();
        GameManager.instance.CutsceneRawImage.ReleaseVideoRenderText();        
    }
}