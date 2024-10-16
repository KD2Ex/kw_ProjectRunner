using UnityEngine;

public class CreaturesFill : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    private Animator animator;

    private readonly int[] anim =
    {
        Animator.StringToHash("0_creatures_indication"),
        Animator.StringToHash("2_creatures_indication"),
        Animator.StringToHash("3_creatures_indication"),
        Animator.StringToHash("6_creatures_indication"),
        Animator.StringToHash("8_creatures_indication")
    };

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        var specificLocation = inventory.Items.Count % 8;
        specificLocation = specificLocation == 0 ? 8 : specificLocation;
        var index = specificLocation / 2 ;
        if (index > inventory.Items.Count) return;
        
        animator.Play(anim[index]);
    }
}
