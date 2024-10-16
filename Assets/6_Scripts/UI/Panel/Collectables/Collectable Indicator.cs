using UnityEngine;

public class CollectableIndicator : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private int FullCount;
    private Animator animator;

    private readonly int select = Animator.StringToHash("Select");
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.SetBool(select, inventory.Items.Count == FullCount);
    }
}
