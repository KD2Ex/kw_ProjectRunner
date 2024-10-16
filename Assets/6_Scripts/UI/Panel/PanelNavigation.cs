using System;
using UnityEngine;

public abstract class PanelNavigation : MonoBehaviour
{
    [SerializeField] protected InputReader input;
    protected UICoord currentPos;
    public UICoord CurrentPos => currentPos;
    protected UISelection[,] selections;

    protected virtual void OnEnable()
    {
        currentPos = new UICoord(0, 0);
        
        input.DisableGameplayInput();
        
        input.UIXMoveEvent += XMove;
        input.UIYMoveEvent += YMove;
        input.InteractEvent += Press;
        Time.timeScale = 0f;
        
        selections[0, 0].Select(true);
    }
    
    protected virtual void OnDisable()
    {
        input.EnableGameplayInput();
        
        input.UIXMoveEvent -= XMove;
        input.UIYMoveEvent -= YMove;
        input.InteractEvent -= Press;
        Time.timeScale = 1f;
        
        selections[0, currentPos.x].Select(false);
    }

    protected virtual void Press()
    {
        Debug.Log(selections[currentPos.y, currentPos.x].name);
        selections[currentPos.y, currentPos.x].Press();
    }

    protected abstract void XMove(int value);
    protected abstract void YMove(int value);
    protected abstract bool IsValid(UICoord coord);
}