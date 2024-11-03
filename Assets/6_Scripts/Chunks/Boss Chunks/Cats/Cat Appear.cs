using System.Collections;
using UnityEngine;

public class CatAppear : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Cats cats;
    
    private Color SpriteColor => sprite.color;

    private void Start()
    {
        StartCoroutine(FadeIn());
        //transform.SetParent(null);
    }

    private IEnumerator FadeIn()
    {
        var alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime;
            sprite.color = new Color(SpriteColor.r, SpriteColor.g, SpriteColor.b, alpha);
            yield return null;
        }
    }

    public void Disappear()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        var alpha = sprite.color.a;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime;
            sprite.color = MathUtils.GetColorWithAlpha(sprite.color, alpha);
            yield return null;
        }
        
        Destroy(gameObject);
        Destroy(cats.gameObject);
    }
    
}
