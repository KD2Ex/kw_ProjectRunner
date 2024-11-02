using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[System.Serializable]
public struct ClampedValue<T>
{
    [Range(0, 10)]
    public T min;
    [Range(0, 10)]
    public T max;
}

public class Milkcap : MonoBehaviour
{
    [SerializeField] private ClampedValue<float> beforeDelay;
    [SerializeField] private ClampedValue<float> afterDelay;

    [SerializeField] private float speed;
    [SerializeField] private float speedDeviation;

    [SerializeField] private Transform cap;
    [SerializeField] private Transform originPoint;
    [SerializeField] private Transform topPoint;

    [SerializeField] private AudioSource sourceUp;
    [SerializeField] private AudioSource sourceDown;

    [FormerlySerializedAs("capCollider")]
    [Space] 
    [Header("Colliders")]
    [SerializeField] private Collider2D capTipCollider;

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
            bool atTop = (topPoint.position - cap.position).magnitude < .01f;
            if (atTop)
            {
                Action after = () => capTipCollider.enabled = false;
                moving = StartCoroutine(MoveTo(originPoint, afterDelay, sourceDown, after: after));
            }
            else
            {
                moving = StartCoroutine(MoveTo(topPoint, beforeDelay, sourceUp, before: () => capTipCollider.enabled = true));
            }
            //var to =  ? originPoint : topPoint;


            //Debug.Log($"Milkcap mag: {(point.position - cap.position).magnitude}");
        }
    }

    private IEnumerator MoveTo(Transform to, ClampedValue<float> delay, AudioSource source, Action before = null, Action after = null)
    {
        var elapsed = 0f;
        var time = Random.Range(delay.min, delay.max);

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        before?.Invoke();
        source.Play();
        //SoundFXManager.instance.PlayClipAtPoint(clip, transform, 1f);

        var speed = Random.Range(this.speed - speedDeviation, this.speed + speedDeviation);
        var dir = (to.position - cap.position);
        while (dir.magnitude > .01f)
        {
            cap.Translate(dir.normalized * (speed * Time.deltaTime));
            dir = (to.position - cap.position);
            yield return null;
        }

        /*
        elapsed = 0f;
        time = Random.Range(afterDelay.min, afterDelay.max);
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        */
        after?.Invoke();
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
