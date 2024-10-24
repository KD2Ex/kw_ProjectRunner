using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Resources;
using UnityEngine;

public class ChunkSpawnManager : MonoBehaviour
{
    [SerializeField] private ChunkRandomManager ChunkRandomManager;
    [SerializeField] private ChunkRuntimeSet RuntimeSet;
    [SerializeField] private CollectablesRuntimeSet CollectablesRuntimeSet;
    [SerializeField] private ChunkMovement chunkMoveComponent;
    [SerializeField] private Chunk FirstChunk;
    private Transform chunkMovement;
    private Transform playerTransform;

    public Transform CurrentChunk { get; private set; }

    private LoadedChunkNames _loadedChunkData;

    public ChunkMovement ChunksPosition => chunkMoveComponent;

    private void Awake()
    {
        //Save(ref chunkData);
        //Debug.Log(chunkData.loadedChunks.Count);
        GameManager.instance.ChunkSpawnManager = this;
    }

    private void OnEnable()
    {
        RuntimeSet.Items.Clear();
    }

    void Start()
    {
        //Debug.Log("Chunk Spawn Manager Start event");
        
        ChunkRandomManager.InitializeChunks();
        
        chunkMovement = GameObject.FindGameObjectWithTag("ChunkParent").transform;
        playerTransform = PlayerLocator.instance.playerTransform;

        Debug.Log(ChunkRandomManager.IsQueueEmpty);

        if (ChunkRandomManager.IsQueueEmpty)
        {
            CreateChunk(FirstChunk);
        }
        
        //Load(chunkData);
        //CreateChunk(ChunkRandomManager.Pop().Chunk);
    }

    void Update()
    {
        if (!CurrentChunk)
        {
            ChunkRandomManager.RestoreAvailability();
            CreateChunk(ChunkRandomManager.Pop().Chunk);
            return;
        }
        
        if (CurrentChunk.position.x - playerTransform.position.x < 20f)
        {
            ChunkRandomManager.RestoreAvailability();
            CreateChunk(ChunkRandomManager.Pop().Chunk);
        }
    }

    private void OnDisable()
    {
        
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
                Debug.Log(prefab.name);
                Debug.Log(chunk.Prefabs.Count);
                InstantiateChunk(prefab);
                continue;
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
        getDestroyed.DistanceToRemove = 15f;
        
        var runtimeItem = new RuntimeChunk();
        runtimeItem.runtimeSet = RuntimeSet;
        runtimeItem.chunk = prefab;
        runtimeItem.instance = instance;
        runtimeItem.Initialize();

        getDestroyed.destroyAction += runtimeItem.Destroy;
        
        if (CurrentChunk)
        {
            instance.transform.position = new Vector3(CurrentChunk.position.x + 36f, 0f, 0f);
        }
        
        runtimeItem.x = instance.transform.localPosition.x;
        CurrentChunk = instance.transform;
    }

    public void ClearQueue()
    {
        ChunkRandomManager.ClearQueue();
    }

    public void Save(ref LoadedChunkNames data)
    {
        if (GameManager.instance.Player.Dead)
        {
            data.Items = new();
            return;
        }
        SaveCurrentChunk(ref data.CurrentChunk);
        
        data.Items = GetGOList();
        
        /*
        if (!Directory.Exists("Assets/Prefabs"))
            AssetDatabase.CreateFolder("Assets", "Prefabs");*/
        /*PrefabUtility.SaveAsPrefabAsset(
            go,
            "Assets/Resources/CurrentSavedChunk/" + name + ".prefab" 
        );*/
        /*var go = RuntimeSet.Items[0].instance;
        string name = go.name + DateTime.Now.Second;

        data.Items[0] = name; */
        
        List<GameObject> GetGOList()
        {
            List<GameObject> result = new();

            foreach (var item in RuntimeSet.Items)
            {
                if (item == RuntimeSet.Items[0]) continue;
                result.Add(item.chunk);
            }

            return result;
        }
        
        
                
        List<string> GetNamesList()
        {
            List<string> result = new();

            foreach (var item in RuntimeSet.Items)
            {
                result.Add(item.chunk.name);
            }

            return result;
        }
    }
    
    public void Load(LoadedChunkNames data)
    {
        /*
        var chunkName = data.Items[0];
        var path = $"CurrentSavedChunk/{chunkName}";
        var currentChunk = Resources.Load<GameObject>(path);
        
        if (!currentChunk) return;
        
        Debug.Log(currentChunk.name);
        currentChunk.transform.localPosition = Vector3.zero;
        ChunkRandomManager.AddChunkToQueue(currentChunk);
        AssetDatabase.DeleteAsset("Resources/" + path);
        */
        
        
        LoadCurrentChunk(data.CurrentChunk);
        foreach (var chunkPrefab in data.Items)
        {
            Debug.Log(chunkPrefab);
            
            // find prefab in Resources by name
            ChunkRandomManager.AddChunkToQueue(chunkPrefab);
        }
        
    }
    
    public void SaveCurrentChunk(ref CurrentChunkData data)
    {
        List<ItemInChunk> result = new();
        
        foreach (var item in CollectablesRuntimeSet.Items)
        {
            result.Add(new ItemInChunk()
            {
                Prefab = item.prefab,
                Position = item.transform.position
            });
        }

        data.Items = result.ToArray();
        data.Prefab = RuntimeSet.Items[0].chunk;

    }
    
    public void LoadCurrentChunk(CurrentChunkData data)
    {
        foreach (var item in data.Items)
        {
            var inst = Instantiate(item.Prefab, item.Position, Quaternion.identity,
                GameManager.instance.ChunkSpawnManager.ChunksPosition.transform);
        }
    }   
}


[System.Serializable]
public struct LoadedChunkNames
{
    public CurrentChunkData CurrentChunk;
    public List<GameObject> Items;
}


[Serializable]
public struct CurrentChunkData
{
    public GameObject Prefab;
    public ItemInChunk[] Items;
}


[Serializable]
public struct ItemInChunk
{
    public GameObject Prefab;
    public Vector2 Position;
}