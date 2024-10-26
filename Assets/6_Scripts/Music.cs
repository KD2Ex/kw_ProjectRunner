using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        GameManager.instance.SceneMusic = this;
    }

    public void SetVolume(float value)
    {
        source.volume = Mathf.Clamp01(value);
    }
}
