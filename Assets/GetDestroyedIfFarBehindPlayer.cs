using System;
using UnityEngine;

public class GetDestroyedIfFarBehindPlayer : MonoBehaviour
{
    [SerializeField] private FloatVariable m_DistanceToDestroy;
    private float Value = 30f;
    private void Awake()
    {
    }

    private Vector3 PlayerPosition => PlayerLocator.instance.playerTransform.position;

    void Update()
    {
        var distance = (PlayerPosition - transform.position).magnitude;
        
        if (distance > Value && PlayerPosition.x > transform.position.x) gameObject.SetActive(false);
    }
}
