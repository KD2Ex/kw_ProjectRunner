using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ChunkSpawnManager : MonoBehaviour
{
    [SerializeField] private ChunkRandomManager ChunkRandomManager;
    [SerializeField] private Chunk FirstChunk;
    private Transform chunkMovement;
    private Transform playerTransform;

    public Transform CurrentChunk { get; private set; }
    
    void Start()
    {
        //Debug.Log("Chunk Spawn Manager Start event");
        
        ChunkRandomManager.InitializeChunks();
        
        chunkMovement = GameObject.FindGameObjectWithTag("ChunkParent").transform;
        playerTransform = PlayerLocator.instance.playerTransform;
        
        CreateChunk(FirstChunk);
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
            List<GameObject> instances = new();
            
            foreach (var prefab in chunk.Prefabs)
            {
                var inst = Instantiate(prefab, chunkMovement);
                instances.Add(inst);
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

    public void ClearQueue()
    {
        ChunkRandomManager.ClearQueue();
    }
    
}
