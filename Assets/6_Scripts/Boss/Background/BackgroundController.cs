using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StopBackground()
    {
        animator.enabled = false;
    }
}
