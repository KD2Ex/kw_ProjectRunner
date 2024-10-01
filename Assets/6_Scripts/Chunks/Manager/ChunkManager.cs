using System;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject loadTrigger;
    [SerializeField] private GameObject deloadTrigger;

    private Transform parent;

    private void Awake()
    {
        parent = GameObject.FindGameObjectWithTag("ChunkParent").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(parent);
        
        var triggerInstance = Instantiate(trigger, transform);

        var loadTriggerInstance = Instantiate(loadTrigger, triggerInstance.transform);
        var deloadTriggerInstance = Instantiate(deloadTrigger, triggerInstance.transform);
        
        deloadTriggerInstance.GetComponent<Trigger>().OnTrigger.AddListener(OnDeloadTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDeloadTrigger()
    {
        gameObject.SetActive(false);
    }
}
