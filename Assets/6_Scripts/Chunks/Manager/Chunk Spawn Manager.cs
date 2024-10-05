using UnityEditor.Compilation;
using UnityEngine;

/*
public abstract class Condition
{
    public abstract bool Evaluate(float delta);
}

public class CounterCondition : Condition
{
    private float value;
    private float compareValue;

    public CounterCondition(float compareValue)
    {
        this.compareValue = compareValue;
    }

    public override bool Evaluate(float delta)
    {
        value += delta;
        return Compare();
    }

    private bool Compare() => value >= compareValue;
}

public class EventCondition : Condition
{
    private bool raised;

    public void Raised()
    {
        raised = true;
    }

    public override bool Evaluate(float delta) => raised;
}
*/

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
        ChunkRandomManager.InitializeChunks();
        
        chunkMovement = GameObject.FindGameObjectWithTag("ChunkParent").transform;
        playerTransform = PlayerLocator.instance.playerTransform;
        
        CreateChunk(ChunkRandomManager.Pop().Chunk);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentChunk.position.x - playerTransform.position.x < 20f)
        {
            ChunkRandomManager.RestoreAvailability();
            CreateChunk(ChunkRandomManager.Pop().Chunk);
        }
    }

    /*private void AddCondition(Chunk chunk)
    {
        Condition instance; 

        switch (chunk.ConditionType)
        {
            case 0:
                instance = new CounterCondition(2f);
                break;
            case 1:
                instance = new EventCondition();
                break;
            default:
                instance = new CounterCondition(0);
                break;
        }
        chunk.SetCondition(instance);
    }*/
    
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
