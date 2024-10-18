using UnityEngine;

public class KarpRay : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private Karp karp;

    private Vector3 dir;
    
    private void OnEnable()
    {
        dir = Vector3.Normalize((target.position - transform.position));
    }

    private void OnDisable()
    {
        karp.ResetAttack();
        transform.position = karp.RayPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerLocator.instance.DistanceToPlayer(transform) > distance) return;
        
        transform.Translate(dir * (speed * Time.deltaTime));
        
        if (transform.position.y < -5f) gameObject.SetActive(false);
    }
    
}
