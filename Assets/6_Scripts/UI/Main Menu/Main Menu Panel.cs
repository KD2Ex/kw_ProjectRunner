using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : PanelNavigation
{
    [SerializeField] private RectTransform hint;
    [SerializeField] private UIStartGameSelection startGame;
    [SerializeField] private UISettingsSelection settings;
    [SerializeField] private UIExitGameSelection exit;

    [SerializeField] private List<RectTransform> hintPos;
    
    private void Awake()
    {
        selections = new UISelection[,] { {startGame, settings, exit } };
    }

    protected override void OnEnable()
    {
        base.OnEnable();

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
        var pos = new UICoord(currentPos.x + value, 0);

        if (!IsValid(pos)) return;

        hint.position = hintPos[currentPos.x].position;
        
        selections[0, currentPos.x].Select(false);
        currentPos = pos;
        selections[0, currentPos.x].Select(true);
    }

    protected override bool IsValid(UICoord coord)
    {
        return coord.x is >= 0 and < 3;
    }
}
