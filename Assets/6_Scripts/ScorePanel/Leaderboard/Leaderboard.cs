using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : ScorePanelElement
{
    [SerializeField] private InputReader input;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private SpriteList sprites;
    [SerializeField] private GameObject pagePrefab;

    private float dir;
    
    private void Awake()
    {
        var yPos = 540;

        //sprites.Items.Reverse();
        
        foreach (var sprite in sprites.Items)
        {
            var inst = Instantiate(pagePrefab, new Vector3(960f, yPos, 0f), Quaternion.identity, transform);
            var instSprite = inst.GetComponent<Image>();
            instSprite.sprite = sprite;
            yPos -= 1080;
        }
    }

    private void OnEnable()
    {
        input.XYMoveEvent += OnMove;
    }

    private void OnDisable()
    {
        input.XYMoveEvent -= OnMove;
    }

    private void Update()
    {
        Move();
    }

    private void OnMove(float dir) => this.dir = dir;
    
    private void Move()
    {
        transform.Translate(new Vector3(0f, dir, 0f) * scrollSpeed);
    }

    public override void Execute()
    {
        gameObject.SetActive(true);
    }

    public override void Stop()
    {
        
    }
}