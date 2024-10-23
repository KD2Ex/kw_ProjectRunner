using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawnManager : MonoBehaviour
{
    [SerializeField] private ChunkRandomManager ChunkRandomManager;
    [SerializeField] private ChunkRuntimeSet RuntimeSet;
    [SerializeField] private Chunk FirstChunk;
    private Transform chunkMovement;
    private Transform playerTransform;

    public Transform CurrentChunk { get; private set; }

    private ChunkSaveData chunkData;

    private void Awake()
    {
        //Save(ref chunkData);
        //Debug.Log(chunkData.loadedChunks.Count);
    }

    void Start()
    {
        //Debug.Log("Chunk Spawn Manager Start event");
        
        ChunkRandomManager.InitializeChunks();
        
        chunkMovement = GameObject.FindGameObjectWithTag("ChunkParent").transform;
        playerTransform = PlayerLocator.instance.playerTransform;
        
        CreateChunk(FirstChunk);
        //Load(chunkData);
        CreateChunk(ChunkRandomManager.Pop().Chunk);
    }

    void Update()
    {
        if (CurrentChunk.position.x - playerTransform.position.x < 20f)
        {
            ChunkRandomManager.RestoreAvailability();
            CreateChunk(ChunkRandomManager.Pop().Chunk);
        }
    }

    private void CreateChunk(Chunk chunk)
    {

        if (chunk.Prefabs.Count == 0)
        {
            InstantiateChunk(chunk.Prefab);
            return;
            
            var instance = Instantiate(chunk.Prefab, chunkMovement);
            
            var getDestroyed = instance.AddComponent<GetDestroyedIfFarBehindPlayer>();
            getDestroyed.SetTarget(playerTransform);
            if (CurrentChunk)
            {
                instance.transform.position = new Vector3(CurrentChunk.position.x + 36f, 0f, 0f);
            }
        
            CurrentChunk = instance.transform;
        }
        else
        {
            
            foreach (var prefab in chunk.Prefabs)
            {
                InstantiateChunk(prefab);
                return;
                var inst = Instantiate(prefab, chunkMovement);
                var getDestroyed = inst.AddComponent<GetDestroyedIfFarBehindPlayer>();
                getDestroyed.SetTarget(playerTransform);
                
                if (CurrentChunk)
                {
                    inst.transform.position = new Vector3(CurrentChunk.position.x + 36f, 0f, 0f);
                }
                CurrentChunk = inst.transform;
            }
        } 
        //chunk.Condition = null; //
    }

    private void InstantiateChunk(GameObject prefab)
    {
        var instance = Instantiate(prefab, chunkMovement);
            
        var getDestroyed = instance.AddComponent<GetDestroyedIfFarBehindPlayer>();
        getDestroyed.SetTarget(playerTransform);
        getDestroyed.DistanceToRemove = 18f;
        
        var runtimeItem = new RuntimeChunk();
        runtimeItem.runtimeSet = RuntimeSet;
        runtimeItem.chunk = prefab;
        runtimeItem.Initialize();

        getDestroyed.destroyAction += runtimeItem.Destroy;
        
        if (CurrentChunk)
        {
            instance.transform.position = new Vector3(CurrentChunk.position.x + 36f, 0f, 0f);
        }
        
        CurrentChunk = instance.transform;
        
    }

    public void ClearQueue()
    {
        ChunkRandomManager.ClearQueue();
    }

    public void Save(ref ChunkSaveData data)
    {
        data.loadedChunks = GetGOList();
        
        List<string> GetGOList()
        {
            List<string> result = new();

            foreach (var item in RuntimeSet.Items)
            {
                result.Add(item.chunk.name);
            }

            return result;
        }
        
        RuntimeSet.Items.Clear();
    }
    
    public void Load(ChunkSaveData data)
    {
        foreach (var chunkPrefab in data.loadedChunks)
        {
            // find prefab in Resources by name
            //ChunkRandomManager.AddChunkToQueue();
        }
        
    }
}
