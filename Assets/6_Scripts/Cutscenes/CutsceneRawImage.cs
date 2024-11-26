using UnityEngine;
using UnityEngine.UI;

public class CutsceneRawImage : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;

    private RenderTexture VideoRenderTexture => rawImage.texture as RenderTexture;
    
    private void Awake()
    {
        Debug.Log("Cutscene RawImage");
        GameManager.instance.CutsceneRawImage = this;
    }

    private void OnEnable()
    {
        ReleaseVideoRenderText();
    }

    private void OnDisable()
    {
        ReleaseVideoRenderText();
    }

    public void ReleaseVideoRenderText()
    {
        VideoRenderTexture.Release();
    }
}