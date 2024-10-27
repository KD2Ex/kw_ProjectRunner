using System.Collections.Generic;
using UnityEngine;

public class PlayerBossController : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [Header("Lines")]
    
    [SerializeField] private GameObject leftLine;
    [SerializeField] private GameObject middleLine;
    [SerializeField] private GameObject rightLine;

    [Header("Components")]
    
    [SerializeField] private Animator animator;
    
    private Dictionary<int, GameObject> lines = new();
    private int currentLine = 1;

    private readonly int animDeathLeft = Animator.StringToHash("LeftDeath");
    private readonly int animDeathRight = Animator.StringToHash("RightDeath");
    
    public bool Dead { get; private set; }
    
    private void Awake()
    {
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
    }
}
