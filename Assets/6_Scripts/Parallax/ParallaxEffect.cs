using System.Collections.Generic;
using UnityEngine;

public enum LayerPosition
{
    Default,
    Top,
    Bottom
}
public class ParallaxEffect : MonoBehaviour
{

    [System.Serializable]
    public struct ParallaxLayer
    {
        public List<Sprite> sprites;
        public List<SpriteRenderer> spriteRenderers;
        public int layer;
        public float speed;
        public LayerPosition layerPosition;
        public float customYPosition;
        public float spawnOffset;
    }


    [SerializeField] private float speed;
    
    [SerializeField] Camera cam;
    [Space]
    [SerializeField] PrefabRender prefab;
    [Space]
    [SerializeField] List<ParallaxLayer> parallaxLayers;

    float camWidth;
    float camHeight;

    bool move;

    int indexTop;




    void Start()
    {
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        foreach (var layer in parallaxLayers)
        {
            PrefabRender newSR = Instantiate(prefab, transform);

            if (layer.layerPosition == LayerPosition.Top) newSR.SetDataStart(layer.layer, null, layer.layerPosition);
            else newSR.SetDataStart(layer.layer, layer.sprites[Random.Range(0, layer.sprites.Count)], layer.layerPosition);
            layer.spriteRenderers.Add(newSR.GetSpriteRenderer());

            newSR.SetPos(Vector2.zero);

            float spawnXPosition = 0f;
            float spawnYPosition = 0f;

            if (layer.layerPosition == LayerPosition.Bottom)
                spawnXPosition = newSR.transform.position.x + camWidth / 2 + newSR.GetSpriteRenderer().bounds.extents.x;
            else spawnXPosition = -camWidth / 2 + newSR.GetSpriteRenderer().bounds.extents.x;

            switch (layer.layerPosition)
            {
                case LayerPosition.Bottom: spawnYPosition = -camHeight / 2 + newSR.GetSpriteRenderer().bounds.extents.y; break;
                case LayerPosition.Top: spawnYPosition = camHeight / 2 - newSR.GetSpriteRenderer().bounds.extents.y; break;
                case LayerPosition.Default: spawnYPosition = layer.spawnOffset; break;
            }
            newSR.transform.position = new Vector2(spawnXPosition, spawnYPosition);
        }
        indexTop++;
    }

    void Update()
    {
        if (!move) return;
        for (int e = 0; e < parallaxLayers.Count; e++)
            MoveLayer(parallaxLayers[e]);
    }


    public void Move(bool e) => move = e;
    void MoveLayer(ParallaxLayer layer)
    {
        for (int e = 0; e < layer.spriteRenderers.Count; e++)
        {
            
            layer.spriteRenderers[e].transform.Translate(Vector2.left * layer.speed * 1f * Time.deltaTime);

            /*if (characterState != null)
            {
            }
            else
            {
                layer.spriteRenderers[e].transform.Translate(Vector2.left * layer.speed * Time.deltaTime);
            }*/

            if (layer.spriteRenderers.Count < 2)
                if (layer.spriteRenderers[0].bounds.max.x < camWidth / 2 + .1f)
                {
                    if (layer.layerPosition == LayerPosition.Bottom) { }
                    if (layer.layerPosition == LayerPosition.Top)
                    {
                        PrefabRender newSR = Instantiate(prefab, transform);
                        newSR.SetDataStart(layer.layer, null, layer.layerPosition, indexTop, layer.spriteRenderers[0].GetComponent<Animator>());
                        layer.spriteRenderers.Add(newSR.GetSpriteRenderer());
                        newSR.SetPos(new Vector2(
                            layer.spriteRenderers[0].bounds.max.x + newSR.GetSpriteRenderer().bounds.extents.x,
                            layer.spriteRenderers[0].transform.position.y));
                        indexTop = indexTop == 1 ? indexTop = 0 : indexTop = 1;
                    }
                    if (layer.layerPosition == LayerPosition.Default)
                    {
                        PrefabRender newSR = Instantiate(prefab, transform);
                        newSR.SetDataStart(layer.layer, layer.sprites[Random.Range(0, layer.sprites.Count)], layer.layerPosition);
                        layer.spriteRenderers.Add(newSR.GetSpriteRenderer());
                        newSR.SetPos(new Vector2(
                            layer.spriteRenderers[0].bounds.max.x + newSR.GetSpriteRenderer().bounds.extents.x,
                            layer.spriteRenderers[0].transform.position.y));
                    }
                }
            if (layer.spriteRenderers[0].bounds.max.x < cam.transform.position.x - camWidth / 2f)
            {
                if (layer.layerPosition == LayerPosition.Bottom)
                {
                    PrefabRender newSR = Instantiate(prefab, transform);
                    newSR.SetDataStart(layer.layer, layer.sprites[Random.Range(0, layer.sprites.Count)], layer.layerPosition);
                    layer.spriteRenderers.Add(newSR.GetSpriteRenderer());
                    newSR.SetPos(new Vector2(
                        cam.transform.position.x + camWidth / 2f + newSR.GetSpriteRenderer().bounds.extents.x,
                        -camHeight / 2 + newSR.GetSpriteRenderer().bounds.extents.y));
                }
                Destroy(layer.spriteRenderers[0].gameObject, .5f);
                layer.spriteRenderers.RemoveAt(0);
            }
        }
    }
}