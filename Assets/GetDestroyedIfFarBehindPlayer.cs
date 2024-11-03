using System;
using UnityEngine;

public class GetDestroyedIfFarBehindPlayer : MonoBehaviour
{
    [SerializeField] private FloatVariable m_DistanceToDestroy;
    [SerializeField] private bool PlayerAsTarget;
    
    private float value = 30f;
    public float Value;
    private Transform target;

    public Action destroyAction;
    [HideInInspector] public float DistanceToRemove;
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
    private void Awake()
    {
    }

    private void Start()
    {
        
        value = m_DistanceToDestroy ? m_DistanceToDestroy.Value : Value;

        if (PlayerAsTarget)
        {
            if (!PlayerLocator.instance) return;
            target = PlayerLocator.instance.playerTransform;
        }
    }

    //private Vector3 PlayerPosition => PlayerLocator.instance.playerTransform.position;
    private Vector3 Pos => target.position;

    void Update()
    {
        if (!target) return;
        
        var distance = (Pos - transform.position).magnitude;
        if (distance > value && Pos.x > transform.position.x)
        {
            gameObject.SetActive(false);
        }
        
        if (distance >= DistanceToRemove && Pos.x > transform.position.x)
        {
            RemoveRuntimeItem();
        }
    }

    private void RemoveRuntimeItem()
    {
        destroyAction?.Invoke();
    }
}
