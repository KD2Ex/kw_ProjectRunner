using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    public static ParallaxManager instance;

    public Parallax DefaultParallax;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}