using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DeathVideo : MonoBehaviour
{
    [SerializeField] private VideoClipsData data;

    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer.Prepare();
    }

    public void Prepare()
    {
        videoPlayer.clip = data.GetRandom();
        videoPlayer.Prepare();
    }
    
    public void Play()
    {
        videoPlayer.Play();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
