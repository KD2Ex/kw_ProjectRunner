using UnityEngine;

public class UpgradeIndicator : MonoBehaviour
{
    [SerializeField] private UpgradeLevel level;
    [SerializeField] private Color color;
    private Color origColor;
    
    private Animator animator;
    private SpriteRenderer sprite;
    

    private int zero = Animator.StringToHash("indicator_zero");
    private int one = Animator.StringToHash("indicator_one");
    private int two = Animator.StringToHash("indicator_two");
    private int three = Animator.StringToHash("indicator_three");
    private int four = Animator.StringToHash("indicator_four");
    private int five = Animator.StringToHash("indicator_five");

    private int[] anims;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        origColor = sprite.color;

        anims = new[]
        {
            zero, one, two, three, four, five,
            one, two, three, four, five
        };
    }

    private void Update()
    {
        sprite.color = level.Value > 5 ? color : origColor;
        animator.Play(anims[level.Value]);
    }
}
