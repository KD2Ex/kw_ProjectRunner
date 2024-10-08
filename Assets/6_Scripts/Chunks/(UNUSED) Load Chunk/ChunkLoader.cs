using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private List<Chunk> m_ChunksToLoad;
    [SerializeField] private ChunkList set;
    [SerializeField] private GameObject triggers;

    private Transform parent;
    
    //private ChunkRandomizer randomizer = ChunkRandomizer.GetInstance();
    
    public void GetAndLoad()
    {
        //var chunk = randomizer.GetRandomChunk(set.Items);
        
        //Load(chunk.Prefab);   
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

        //SyncInstantiate(prefab, pos);
        var obj = InstantiateAsync(prefab, pos, Quaternion.identity);
        StartCoroutine(Loading(obj));
    }

    private IEnumerator Loading(AsyncInstantiateOperation<GameObject> chunk)
    {
        yield return new WaitUntil(() => chunk.isDone);
        
        var go = chunk.Result[0];
        // not sure that this is the best practice, need more experience with Async operations
        go.transform.position = new Vector3(transform.position.x + 18f, transform.position.y, 0f);
        Instantiate(triggers, go.transform);
        go.SetActive(true);
    }

    private void SyncInstantiate(GameObject prefab, Vector3 pos)
    {
        var instance = Instantiate(prefab, pos, Quaternion.identity);
        instance.SetActive(true);
    }
}
