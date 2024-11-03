using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnavalChunk : MonoBehaviour
{
    public CarnavalRuntimeSet set;

    private float DistToPlayer => (transform.position.x - GameManager.instance.Player.transform.position.x);
    private bool lastChunk;

    private Parallax defaultPar => ParallaxManager.instance.DefaultParallax;

    private List<GameObject> carnavalBacks => set.backs;

    private void OnEnable()
    {
       
        if (set.Items.Count == 0)
        {
            GameManager.instance.CarnavalCrashedBalloons = 0;
            
            set.originBacks = defaultPar.m_Backgrounds;
            defaultPar.m_Backgrounds = carnavalBacks;
            defaultPar.CalculateBounds();
        }
        if (set.Items.Count == set.maxCount - 1)
        {
            lastChunk = true;
        }
        set.Add(this);
        
    }

    private IEnumerator WaitForPlayer()
    {
        yield return new WaitUntil(() => DistToPlayer < 25f);
        ParallaxManager.instance.DefaultParallax.m_Backgrounds = set.originBacks;
        defaultPar.CalculateBounds();
    }

    private void Start()
    {
        if (!lastChunk) return;
        
        StartCoroutine(WaitForPlayer());
        
        set.Items.Clear();
    }
}