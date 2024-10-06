using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    /*[SerializeField] private GameObject trigger;
    [SerializeField] private GameObject loadTrigger;
    [SerializeField] private GameObject deloadTrigger;*/

    [SerializeField] private ChunkRandomManager ChunkRandomManager;
    
    private Transform parent;
    private Transform playerTransform;

    private bool triggered;

    private void Awake()
    {
        parent = GameObject.FindGameObjectWithTag("ChunkParent").transform; // Get rig of FindGameObject
    }

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        return;
        transform.SetParent(parent);
        
        /*var triggerInstance = Instantiate(trigger, transform);

        var loadTriggerInstance = Instantiate(loadTrigger, triggerInstance.transform);
        var deloadTriggerInstance = Instantiate(deloadTrigger, triggerInstance.transform);*/
        
        playerTransform = PlayerLocator.instance.playerTransform;
        
        //deloadTriggerInstance.GetComponent<Trigger>().OnTrigger.AddListener(OnDeloadTrigger);
    }


    private void Update()
    {
        return;
        var distanceToPlayer = (playerTransform.position - transform.position).magnitude;
        var rightSide = transform.position.x > playerTransform.position.x;

        if (!triggered && distanceToPlayer < 20f && rightSide)
        {
            triggered = true;
            Instantiate(ChunkRandomManager.Pop().Chunk.Prefab);
        }        
    }
}
