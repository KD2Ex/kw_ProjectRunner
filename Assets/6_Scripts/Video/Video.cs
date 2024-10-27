using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    /*
    [SerializeField] private GameObject mp4Holder;

    public void Execute()
    {
        mp4Holder.SetActive(true);
    }
    */
    
    [SerializeField] private VideoPlayer video;

    private bool start;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(video.length);
    }

    // Update is called once per frame
    void Update()
    {
        if (start) return;
        
        Debug.Log(video.time);

        if (video.time > 0)
        {
            start = true;
            
        }
    }
    
    
}
