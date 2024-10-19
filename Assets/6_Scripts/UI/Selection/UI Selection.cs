using System.Resources;
using UnityEngine;

public abstract class UISelection : MonoBehaviour
{
    private Animator animator;
    private readonly int animSelect = Animator.StringToHash("Select");

    [SerializeField] private GameObject tipButton;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();

        Debug.Log($"{animator}");
        Debug.Log($"{gameObject.name} ");
        Debug.Log($"{animSelect} ");
    }

    public virtual void Select(bool value)
    {
        if (!animator) return;
        animator.SetBool(animSelect, value);

        if (tipButton)
        {
            tipButton.SetActive(value);
        }
    }
    public abstract void Press();
}
