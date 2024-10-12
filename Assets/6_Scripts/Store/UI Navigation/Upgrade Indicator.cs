using UnityEngine;

public class UpgradeIndicator : MonoBehaviour
{
    [SerializeField] private UpgradeLevel level;

    private Animator animator;

    private int zero = Animator.StringToHash("indicator_zero");
    private int one = Animator.StringToHash("indicator_one");
    private int two = Animator.StringToHash("indicator_two");
    private int three = Animator.StringToHash("indicator_three");
    private int four = Animator.StringToHash("indicator_four");
    private int five = Animator.StringToHash("indicator_five");

    private int[] anims;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        anims = new[] {zero, one, two, three, four, five};
    }

    private void Update()
    {
        animator.Play(anims[level.Value]);
    }
}
