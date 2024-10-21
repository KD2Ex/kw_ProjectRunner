using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float distance;
    protected Animator animator;
    protected float DistanceToPlayer => PlayerLocator.instance.DistanceToPlayer(transform);
    protected bool PlayerInDistance => DistanceToPlayer <= distance;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}