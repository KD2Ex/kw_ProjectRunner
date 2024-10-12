using UnityEngine;

public abstract class UISelection : MonoBehaviour
{
    private Animator animator;
    private readonly int animSelect = Animator.StringToHash("Select");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Select(bool value)
    {
        animator.SetBool(animSelect, value);
    }
    public abstract void Press();
}
