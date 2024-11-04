using System.Collections;
using UnityEngine;

public class GodOnLocation : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float DistanceToTarget => target.position.x - transform.position.x;

    readonly WaitForFixedUpdate waiter = new ();
    private Vector3 origin;
    
    private void Start()
    {
        origin = transform.position;
        
        transform.SetParent(null);
        target.SetParent(null);
        
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while (DistanceToTarget > .1f) 
        {
            transform.Translate(Vector2.right * (5f * Time.fixedDeltaTime));
            yield return waiter;
        }
    }

    public void Disappear()
    {
        StartCoroutine(MoveBack());
    }

    private IEnumerator MoveBack()
    {
        float dist;
        var orig = new Vector3(transform.position.x - 7f, transform.position.y, 0f);
        
        do
        {
            dist = (transform.position - orig).magnitude;

            Debug.Log(dist);
            
            transform.Translate(Vector2.left * (5f * Time.fixedDeltaTime));
            yield return waiter;
        } while (dist > .1f);
        
        Destroy(gameObject);
    }
}