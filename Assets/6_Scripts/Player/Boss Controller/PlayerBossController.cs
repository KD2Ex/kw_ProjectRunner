using System.Collections.Generic;
using UnityEngine;

public class PlayerBossController : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [Header("Lines")]
    
    [SerializeField] private GameObject leftLine;
    [SerializeField] private GameObject middleLine;
    [SerializeField] private GameObject rightLine;

    private Dictionary<int, GameObject> lines = new();
    private int currentLine = 1;
    
    private void Awake()
    {
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
        input.PlayerBossMoveEvent += OnMove;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
