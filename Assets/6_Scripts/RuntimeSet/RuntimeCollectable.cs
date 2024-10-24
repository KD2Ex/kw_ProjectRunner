using UnityEngine;

public class RuntimeCollectable : MonoBehaviour
{
    public CollectablesRuntimeSet set;

    public GameObject prefab;
    
    private void Awake()
    {
        if (PlayerLocator.instance.DistanceToPlayer(transform) < 14f)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Debug.Log(PlayerLocator.instance.DistanceToPlayer(transform));
        
        if (PlayerLocator.instance.DistanceToPlayer(transform) < -5f)
        {
            set.Remove(this);
            enabled = false;
        }
        
        if (PlayerLocator.instance.DistanceToPlayer(transform) < 14f)
        {
            set.Add(this);
        }
    }

    private void OnEnable()
    {
        
    }
}