using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ApproachingButton : MonoBehaviour
{
    [SerializeField] private float fadeRate;
    private SpriteRenderer sprite;
    private Image image;

    private Color color;
    private float colorAlpha = 0f;
    
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();

        image.color = MathUtils.GetColorWithAlpha(image.color, 0f);
    }

    private void Update()
    {
        /*if (image)
        {
            image.color = GetColor(image.color, colorAlpha);
        }

        if (sprite)
        {
            sprite.color = GetColor(sprite.color, colorAlpha);
        }*/
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(Coroutines.FadeUIImage(image.color.a, 1f, image, fadeRate));
        //StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(Coroutines.FadeUIImage(image.color.a, 0f, image, fadeRate));
        //StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        if (sprite)
        {
            if (Mathf.Approximately(to, sprite.color.a)) yield break;
        }
        if (from > to)
        {
            while (from > to)
            {
                Down();
                if (Mathf.Approximately(from, to)) yield break;
                colorAlpha = from;
                yield return null;
            }

            colorAlpha = to;
            yield break;
        }
        
        while (from < to)
        {
            Up();
            if (Mathf.Approximately(from, to)) yield break;
            colorAlpha = from;
            yield return null;
        }

        colorAlpha = to;
        yield break;


        void Up()
        {
            from += Time.deltaTime * fadeRate;
        }

        void Down()
        {
            from -= Time.deltaTime * fadeRate;
        }
    }

    private Color GetColor(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
