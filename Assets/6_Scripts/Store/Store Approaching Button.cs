using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StoreApproachingButton : MonoBehaviour
{
    [SerializeField] private float FadeRate;
    
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        if (Mathf.Approximately(to, sprite.color.a)) yield break;
        if (from > to)
        {
            while (from > to)
            {
                Down();
                if (Mathf.Approximately(from, to)) yield break;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, from);
                yield return null;
            }
            yield break;
        }
        
        while (from < to)
        {
            Up();
            if (Mathf.Approximately(from, to)) yield break;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, from);
            yield return null;
        }
        yield break;


        void Up()
        {
            from += Time.deltaTime * FadeRate;
        }

        void Down()
        {
            from -= Time.deltaTime * FadeRate;
        }
    }
}
