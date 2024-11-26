using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBossController : MonoBehaviour, IInvincible
{
    [SerializeField] private InputReader input;
    [SerializeField] private FloatVariable healths;
    
    [Header("Lines")]
    
    [SerializeField] private GameObject leftLine;
    [SerializeField] private GameObject middleLine;
    [SerializeField] private GameObject rightLine;

    [Header("Components")]
    
    [SerializeField] private Animator animator;
    [SerializeField] private InvincibilityController invincibilityController;
    
    private Dictionary<int, GameObject> lines = new();
    private int currentLine = 1;

    private readonly int animDeathLeft = Animator.StringToHash("LeftDeath");
    private readonly int animDeathRight = Animator.StringToHash("RightDeath");

    
    public bool Dead { get; private set; }

    public UnityEvent OnLoseHeath;
    public UnityEvent OnDeath;
    public UnityEvent OnDeathAnimationEnd;
    
    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        GameManager.instance.PlayerBossController = this;

        lines[0] = leftLine;
        lines[1] = middleLine;
        lines[2] = rightLine;
    }

    private void OnEnable()
    {
        input.PlayerBossMoveEvent += OnMove;
    }

    private void OnDisable()
    {
        input.PlayerBossMoveEvent -= OnMove;
    }

    private void OnMove(int value)
    {
        Debug.Log($"Boss movement value: {value}");

        currentLine += value;
        currentLine = Mathf.Clamp(currentLine, 0, 2);
        
        MoveOnLine(lines[currentLine]);
    }

    private void MoveOnLine(GameObject line)
    {
        transform.position = line.transform.position;
    }

    public void Die()
    {
        var hash = currentLine > 0 ? animDeathRight : animDeathLeft;
        animator.Play(hash);
        input.PlayerBossMoveEvent -= OnMove;
        Dead = true;
        OnDeath?.Invoke(); 
        StartCoroutine(Coroutines.WaitFor(2.5f, null, () => OnDeathAnimationEnd?.Invoke()));
    }

    public void CheckHealth()
    {
        if (healths.Value > 0)
        {
            healths.Value--;
            invincibilityController.Trigger();
            OnLoseHeath?.Invoke();
            return;
        }
        
        Die();
    }

    public SpriteRenderer Sprite { get; set; }
    public bool IsInvincible()
    {
        return false;
    }
}
