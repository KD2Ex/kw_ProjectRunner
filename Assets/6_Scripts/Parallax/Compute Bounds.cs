using System.Collections.Generic;
using UnityEngine;

public class ComputeBounds : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> sprites;

    public Bounds GetBounds()
    {
        var x = 0f;
        
        foreach (var sprite in sprites)
        {
            x += sprite.bounds.size.x;
        }

        return new Bounds(Vector3.zero, new Vector3(x, 0f, 0f));
    }
}
