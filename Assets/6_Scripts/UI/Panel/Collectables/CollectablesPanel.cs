using System;
using UnityEngine;

public class CollectablesPanel : PanelNavigation
{
    [SerializeField] private InputReader input;
    [SerializeField] private ExitToMenuSelection exit;
    private void Awake()
    {
        currentPos = new UICoord(0, 0);
        
        selections = new UISelection[,] { {exit} };
    }

    private void OnEnable()
    {
        input.DisableGameplayInput();
        input.InteractEvent += Press;
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        input.EnableGameplayInput();
        input.InteractEvent -= Press;
        Time.timeScale = 1f;
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
        
    }

    protected override void IsValid(UICoord coord)
    {
        
    }
}
