using UnityEngine;
using UnityEngine.Video;

public class DeathVideo : ScorePanelElement
{
    [SerializeField] private VideoClipsData data;
    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private DeathVideoHintButton button;
    
    private void OnEnable()
    {
        Prepare();
    }

    private void Prepare()
    {
        GameManager.instance.CutsceneRawImage.ReleaseVideoRenderText();
        videoPlayer.clip = data.GetRandom();
        videoPlayer.Prepare();
    }
    
    private void Play()
    {
        videoPlayer.Play();
    }

    public override void Execute()
    {
        Play();
        button.gameObject.SetActive(true);
    }

    public override void Stop()
    {
        videoPlayer.Stop();
        GameManager.instance.CutsceneRawImage.ReleaseVideoRenderText();
        
        button.gameObject.SetActive(false);
    }
}
