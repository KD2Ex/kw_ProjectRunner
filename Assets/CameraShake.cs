using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private bool start;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;

    private Vector3 originPos;
    
    private void Awake()
    {
        originPos = transform.position;
        GameManager.instance.CameraShake = this;
    }

    private void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    public void Execute()
    {
        StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        var startPos = originPos;
        var elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            
            var random = Random.insideUnitSphere;
            var force = curve.Evaluate(elapsed / duration);
            
            transform.position = startPos + random * force;
            yield return null;
        }

        transform.position = startPos;
    }
    
}