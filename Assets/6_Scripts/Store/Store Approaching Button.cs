using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ApproachingButton : MonoBehaviour
{
    [SerializeField] private float fadeRate;
    private SpriteRenderer sprite;
    private Image image;

    private Color color;
    private float colorAlpha = 0f;

    private bool isImage;
    
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();

        if (image)
        {
            isImage = true;
            color = image.color;
            image.color = MathUtils.GetColorWithAlpha(image.color, 0f);
            return;
        }

        if (sprite)
        {
            isImage = false;
            color = sprite.color;
            sprite.color = MathUtils.GetColorWithAlpha(color, 0f);
        }
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
        if (!gameObject.activeSelf) return;

        if (image)
        {
            Debug.Log(image.color.a);
        }
        
        StopAllCoroutines();
        var color = isImage ? image.color : sprite.color;
        StartCoroutine(Coroutines.FadeFloat(color.a, 1f, fadeRate, SetColor));
        //StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOut()
    {
        if (!gameObject.activeSelf) return;
        
        if (image)
        {
            Debug.Log(image.color.a);
        }

        
        StopAllCoroutines();
        var color = isImage ? image.color : sprite.color;
        StartCoroutine(Coroutines.FadeFloat(color.a, 0f, fadeRate, SetColor));
        //StartCoroutine(Fade(1f, 0f));
    }

    private void SetColor(float alpha)
    {
        if (isImage)
        {
            image.color = MathUtils.GetColorWithAlpha(image.color, alpha);
            return;
        }

        sprite.color = MathUtils.GetColorWithAlpha(sprite.color, alpha);
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
