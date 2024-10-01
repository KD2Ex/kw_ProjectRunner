using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private List<Chunk> m_ChunksToLoad;
    [SerializeField] private StartChunk set;

    private Transform parent;
    
    private ChunkRandomizer randomizer = ChunkRandomizer.GetInstance();
    
    public void GetAndLoad()
    {
        var chunk = randomizer.GetRandomChunk(set.Items);
        
        Load(chunk.Prefab);   
    }

    public void LoadAll()
    {
        foreach (var chunk in m_ChunksToLoad)
        {
            Load(chunk.Prefab);
        }
    }
    
    public void Load(GameObject prefab)
    {
        var pos = new Vector3(transform.position.x + 18f, transform.position.y, 0f);

        var obj = InstantiateAsync(prefab, pos, Quaternion.identity);
        StartCoroutine(Loading(obj));
    }

    private IEnumerator Loading(AsyncInstantiateOperation<GameObject> chunk)
    {
        yield return new WaitUntil(() => chunk.isDone);
        
        var go = chunk.Result[0];
        go.SetActive(true);
    }
}
