using UnityEngine;

public abstract class UISelection : MonoBehaviour
{
    protected Animator animator;
    protected readonly int animSelect = Animator.StringToHash("Select");

    [SerializeField] private GameObject tipButton;
    [SerializeField] protected SoundList sounds;

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

    public virtual void Press()
    {
        SoundFXManager.instance.PlayClipAtPoint(sounds.GetRandom(), transform, 1f);
    }
}
