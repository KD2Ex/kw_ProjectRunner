using System.Collections;
using UnityEngine;

public class VictimChunk : MonoBehaviour
{
    [SerializeField] private SpriteRenderer parallaxLayer;

    private Parallax parallax => ParallaxManager.instance.DefaultParallax;
    
    private void OnEnable()
    {
        //StartCoroutine(WaitForSpawn());
    }

    private IEnumerator WaitForSpawn()
    {

        Debug.Log($"Child count {parallax.transform.childCount}");
        
        yield return new WaitUntil(() => parallax.transform.childCount >= 2);
        
        var lastLayer = ParallaxManager.instance.DefaultParallax.transform.GetChild(parallax.transform.childCount - 1);

        Debug.Log(lastLayer.name);
        lastLayer.TryGetComponent<SpriteRenderer>(out var sprite);

        if (sprite)
        {
            sprite.sprite = parallaxLayer.sprite;
            yield break;
        }

        for (int i = 0; i < lastLayer.childCount; i++)
        {
            var child = lastLayer.GetChild(i);
            child.gameObject.SetActive(false);
        }
        
        var parTrans = parallaxLayer.transform;
        var pos = parTrans.position;
        parTrans.SetParent(lastLayer);
        parTrans.localPosition = new Vector3(7.83f, pos.y, 0f); // wtf is 7.83f? idk.
        parallaxLayer.gameObject.SetActive(true);
    }
}
