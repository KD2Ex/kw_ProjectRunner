using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private readonly int hash = Animator.StringToHash("Click");
    
    void Start()
    {
        animator.SetTrigger(hash);    
    }
}
