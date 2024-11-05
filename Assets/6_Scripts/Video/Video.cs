using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{

    [SerializeField] private CutScene cutScene;
    [SerializeField] private VideoPlayer video;

    private bool start;
    
    void Start()
    {
        Debug.Log(video.length);

        if (cutScene)
        {
            StartCoroutine(WaitForEnd());
        }
    }

    private IEnumerator WaitForEnd()
    {
        yield return new WaitForSecondsRealtime(Convert.ToSingle(video.length / video.playbackSpeed));
        cutScene.Play();
    }

    void Update()
    {
        if (start) return;
        
        if (video.time > 0)
        {
            start = true;
        }
    }
}
