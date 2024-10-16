using System;
using UnityEngine;

public class GetDestroyedIfFarBehindPlayer : MonoBehaviour
{
    [SerializeField] private FloatVariable m_DistanceToDestroy;
    [SerializeField] private bool PlayerAsTarget;

    private float Value = 30f;

    private Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
    private void Awake()
    {
    }

    private void Start()
    {
        Value = m_DistanceToDestroy ? m_DistanceToDestroy.Value : 30f;
        
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
        if (distance > Value && Pos.x > transform.position.x) gameObject.SetActive(false);
    }
}
