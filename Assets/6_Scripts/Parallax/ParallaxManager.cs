using System;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    public static ParallaxManager instance;

    public Parallax DefaultParallax;
    public Parallax CarnavalParallax;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}