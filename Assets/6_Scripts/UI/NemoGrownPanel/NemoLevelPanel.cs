using UnityEngine;

public class NemoLevelPanel : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private void OnEnable()
    {
        source.Play();
    }

    private void OnDisable()
    {
        source.Stop();
    }
}