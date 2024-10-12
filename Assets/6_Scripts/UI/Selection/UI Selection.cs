using System.Resources;
using UnityEngine;

public abstract class UISelection : MonoBehaviour
{
    private Animator animator;
    private readonly int animSelect = Animator.StringToHash("Select");

    [SerializeField] private GameObject tipButton;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Select(bool value)
    {
        animator.SetBool(animSelect, value);

        if (tipButton)
        {
            tipButton.SetActive(value);
        }
    }
    public abstract void Press();
}
