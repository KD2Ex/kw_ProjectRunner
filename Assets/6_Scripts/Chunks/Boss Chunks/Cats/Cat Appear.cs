using System.Collections;
using UnityEngine;

public class CatAppear : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private Color SpriteColor => sprite.color;
    
    private void OnEnable()
    {
        StartCoroutine(FadeIn());
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
