using UnityEngine;
using UnityEngine.UI;

public class CutsceneRawImage : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;

    private RenderTexture VideoRenderTexture => rawImage.texture as RenderTexture;
    
    private void Awake()
    {
        GameManager.instance.CutsceneRawImage = this;
    }

    public void ReleaseVideoRenderText()
    {
        VideoRenderTexture.Release();
    }
}