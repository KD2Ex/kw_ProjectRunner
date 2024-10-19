using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ClampedValue<T>
{
    [Range(0, 10)]
    public T min;
    [Range(0, 10)]
    public T max;
}

public class Milkcup : MonoBehaviour
{
    [SerializeField] private ClampedValue<float> beforeDelay;
    [SerializeField] private ClampedValue<float> afterDelay;
    
    [SerializeField] private float speed;
    [SerializeField] private float speedDeviation;
    
    [SerializeField] private Transform cap;
    [SerializeField] private Transform capPoint;
    [SerializeField] private Transform point;
    
    private Transform player => PlayerLocator.instance.playerTransform;

    private float distance => (transform.position - player.position).magnitude;
    private List<int> randomPool = new();

    private Coroutine moving;
    
    // Start is called before the first frame update
    void Start()
    {
        RefillPool();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == null)
        {
            var to = (point.position - cap.position).magnitude < .01f ? capPoint : point;

            //Debug.Log($"Milkcap mag: {(point.position - cap.position).magnitude}");
            moving = StartCoroutine(Move(to));
        }
    }

    private IEnumerator Move(Transform to)
    {
        var elapsed = 0f;
        var time = Random.Range(beforeDelay.min, beforeDelay.max);
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        var speed = Random.Range(this.speed - speedDeviation, this.speed);
        var dir = (to.position - cap.position);
        while (dir.magnitude > .01f)
        {
            cap.Translate(dir.normalized * (speed * Time.deltaTime));
            dir = (to.position - cap.position);
            yield return null;
        }

        elapsed = 0f;
        time = Random.Range(afterDelay.min, afterDelay.max);
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        moving = null;
    }

    private void RefillPool()
    {
        randomPool.Clear();
        for (int i = 0; i < 4; i++)
        {
            randomPool.Add(Random.Range(0, 2));
            //Debug.Log(randomPool[i]);
        }
    }
}
