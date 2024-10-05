using UnityEngine;

public class ChunkSpawnManager : MonoBehaviour
{
    [SerializeField] private ChunkRandomManager ChunkRandomManager;
    
    private Transform chunkMovement;
    private Transform playerTransform;

    public Transform CurrentChunk { get; private set; }
    
    void Start()
    {
        ChunkRandomManager.InitializeChunks();
        
        chunkMovement = GameObject.FindGameObjectWithTag("ChunkParent").transform;
        playerTransform = PlayerLocator.instance.playerTransform;
        
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
        var instance = Instantiate(chunk.Prefab, chunkMovement);
        //chunk.Condition = null; //
        instance.AddComponent<GetDestroyedIfFarBehindPlayer>();
        if (CurrentChunk)
        {
            instance.transform.position = new Vector3(CurrentChunk.position.x + 36f, 0f, 0f);
        }
        
        CurrentChunk = instance.transform;
    }

}
