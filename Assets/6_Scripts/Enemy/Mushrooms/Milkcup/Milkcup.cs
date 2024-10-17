using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milkcup : MonoBehaviour
{
    [SerializeField] private float min;
    [SerializeField] private float max;

    [SerializeField] private float speed;
    [SerializeField] private float speedDeviation;
    
    [SerializeField] private Transform cap;
    [SerializeField] private Transform point;
    
    private Transform player => PlayerLocator.instance.playerTransform;

    private float distance => (transform.position - player.position).magnitude;
    private float randDist;

    private bool shouldBeExecuted;

    private List<int> randomPool = new();
    
    
    // Start is called before the first frame update
    void Start()
    {
        RefillPool();
        randDist = Random.Range(min, max);
        
        shouldBeExecuted = Random.Range(0, 2) == 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.distance > randDist) return;

        if (!shouldBeExecuted) return;
        
        Execute();
        
    }

    private void Execute()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        var dir = (point.position - cap.position);
        var speed = Random.Range(this.speed - speedDeviation, this.speed);
        
        while (dir.magnitude > .3f)
        {
            cap.Translate(dir.normalized * (speed * Time.deltaTime));
            dir = (point.position - cap.position);
            yield return null;
        }
    }

    private void RefillPool()
    {
        randomPool.Clear();
        for (int i = 0; i < 4; i++)
        {
            randomPool.Add(Random.Range(0, 2));
            Debug.Log(randomPool[i]);
        }
    }
}
