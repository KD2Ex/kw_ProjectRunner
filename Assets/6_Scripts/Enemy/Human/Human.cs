using UnityEngine;

public class Human : Enemy
{
    [SerializeField] private float speed;

    private void Start()
    {
        //transform.SetParent(null);
    }

    private void Update()
    {
        if (PlayerLocator.instance.DistanceToPlayer(transform) < distance)
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
    }
}
