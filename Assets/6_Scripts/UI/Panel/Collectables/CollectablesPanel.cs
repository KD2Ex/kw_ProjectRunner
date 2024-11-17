using System;
using UnityEngine;
using UnityEngine.Events;

public class CollectablesPanel : PanelNavigation
{
    [SerializeField] private Coins coins;
    
    [SerializeField] private UIBackToGame backToGame;
    [SerializeField] private UIOpenNemoPanel openNemo;
    [SerializeField] private ExitToMenuSelection exit;

    public UnityEvent Open;
    public UnityEvent Closed;

    private bool nemoOpened => GameManager.instance.NemoOpened;
    
    private void Awake()
    {
        selections = new UISelection[,] { {backToGame, openNemo, exit } };
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        Open?.Invoke();

        openNemo.gameObject.SetActive(nemoOpened);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Closed?.Invoke();
    }

    private void Start()
    {
        selections[0, 0].Select(true);
    }

    protected override void XMove(int value)
    {
        
    }

    protected override void YMove(int value)
    {
        var pos = new UICoord(currentPos.x + value, 0);
        Debug.Log($"Pos x: {pos.x},, {nemoOpened}");

        if (pos.x == 1 && !nemoOpened)
        {
            Debug.Log("wtf");
            pos = new UICoord(pos.x + value, 0);
        }  
        if (!IsValid(pos)) return;
        Debug.Log($"new Pos x: {pos.x}");

        selections[0, currentPos.x].Select(false);
        currentPos = pos;
        selections[0, currentPos.x].Select(true);
    }

    protected override bool IsValid(UICoord coord)
    {
        return coord.x is >= 0 and < 3;
    }
}
