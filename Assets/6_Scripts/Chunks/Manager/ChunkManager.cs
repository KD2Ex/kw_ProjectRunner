using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject loadTrigger;
    [SerializeField] private GameObject deloadTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        var triggerInstance = Instantiate(trigger, transform);

        var loadTriggerInstance = Instantiate(loadTrigger, triggerInstance.transform);
        var deloadTriggerInstance = Instantiate(deloadTrigger, triggerInstance.transform);
        
        loadTriggerInstance.GetComponent<Trigger>().OnTrigger.AddListener(OnLoadTrigger);
        deloadTriggerInstance.GetComponent<Trigger>().OnTrigger.AddListener(OnDeloadTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLoadTrigger()
    {
        // get random chunk
        
    }
    
    private void OnDeloadTrigger()
    {
        gameObject.SetActive(false);
    }
}
