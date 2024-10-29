using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Balloon : MonoBehaviour
{
    [SerializeField] private Fox fox;
    [SerializeField] private List<AudioClip> clips;

    private Player player => GameManager.instance.Player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        SoundFXManager.instance.PlayClipAtPoint(clips[Random.Range(0, clips.Count)], transform, 1f);
        gameObject.SetActive(false);

        if (player.Invincible) return; 
        
        var foxInstance = FindObjectOfType<Fox>();
        if (foxInstance)
        {
            foxInstance.GetCloser();
        }
        else
        {
            fox.gameObject.SetActive(true);
        }
    }
}
