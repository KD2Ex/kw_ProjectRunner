using UnityEngine;

public class ChunkSpawnManager : MonoBehaviour
{
    [SerializeField] private ChunkRandomManager ChunkRandomManager;
    
    private Transform chunkMovement;
    private Transform playerTransform;

    public Transform CurrentChunk { get; private set; }
    
    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        chunkMovement = GameObject.FindGameObjectWithTag("ChunkParent").transform;
        playerTransform = PlayerLocator.instance.playerTransform;
        CreateChunk(ChunkRandomManager.Pop().Chunk);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentChunk.position.x - playerTransform.position.x < 20f)
        {
            CreateChunk(ChunkRandomManager.Pop().Chunk);
        }
    }
    
    private void CreateChunk(Chunk chunk)
    {
        var instance = Instantiate(chunk.Prefab, chunkMovement);

        instance.AddComponent<GetDestroyedIfFarBehindPlayer>();
        if (CurrentChunk)
        {
            instance.transform.position = new Vector3(CurrentChunk.position.x + 36f, 0f, 0f);
        }
        
        CurrentChunk = instance.transform;
    }

}
