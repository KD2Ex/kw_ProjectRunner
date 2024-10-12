using System.Collections;
using UnityEngine;

public class StoreApproachingButton : MonoBehaviour
{
    [SerializeField] private float FadeRate;
    
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
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
                sprite.color = sprite.color = GetColor(sprite.color, from);
                yield return null;
            }

            sprite.color = GetColor(sprite.color, to);
            yield break;
        }
        
        while (from < to)
        {
            Up();
            if (Mathf.Approximately(from, to)) yield break;
            sprite.color = sprite.color = GetColor(sprite.color, from);
            yield return null;
        }

        sprite.color = GetColor(sprite.color, to);
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

    private Color GetColor(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
