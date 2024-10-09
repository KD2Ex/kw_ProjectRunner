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
        StopCoroutine(Fade(0f, 1f));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StopCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        while (from < to)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, from);
            from += Time.deltaTime * FadeRate;
            yield return null;
        }
    }
}
