using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private Fox fox;
    [SerializeField] private List<AudioClip> clips;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        var foxInstance = FindObjectOfType<Fox>();
        if (foxInstance)
        {
            //    
            foxInstance.GetCloser();
        }
        else
        {
            fox.gameObject.SetActive(true);
        }
        
        SoundFXManager.instance.PlayClipAtPoint(clips[Random.Range(0, clips.Count)], transform, 1f);
        gameObject.SetActive(false);
    }
}
