using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private bool start;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;

    private void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    private IEnumerator Shaking()
    {
        var startPos = transform.position;
        var elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            
            var random = Random.insideUnitSphere;
            random.y = 0f;
            
            var force = curve.Evaluate(elapsed / duration);
            
            transform.position = startPos + random * force;
            yield return null;
        }

        transform.position = startPos;
    }
    
}