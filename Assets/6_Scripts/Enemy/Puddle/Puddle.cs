using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField] private float distance;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (PlayerLocator.instance.DistanceToPlayer(transform) < distance)
        {
            animator.Play("puddle");
        }
    }
}
