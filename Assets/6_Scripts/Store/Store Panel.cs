using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    UP,DOWN
}

public class StorePanel : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private List<UIBoosterUpgradeSelection> upgradeSelections;
    [SerializeField] private UIExitSelection exitSelection;
    [SerializeField] private UIAddHealthSelection healthSelection;

    private UICoord currentPos;

    private const int columns = 4;
    private const int rows = 2;

    private UISelection[,] selections;
    
    private void Awake()
    {
        currentPos = new UICoord(3, 0);
        selections = new UISelection[rows, columns];

        for (int i = 0; i < upgradeSelections.Count - 1; i++)
        {
            selections[0, i] = upgradeSelections[i];
        }

        selections[0, 3] = exitSelection;
        selections[1, 0] = healthSelection;
        selections[1, 2] = upgradeSelections[3];
        
    }

    private void OnEnable()
    {
        input.DisableGameplayInput();
        input.InteractEvent += Press;
        input.UIXMoveEvent += XMove;
        input.UIYMoveEvent += YMove;
    }

    private void OnDisable()
    {
        input.EnableGameplayInput();
        input.InteractEvent -= Press;
        input.UIXMoveEvent -= XMove;
        input.UIYMoveEvent -= YMove;
    }

    private void Start()
    {
        selections[0, 3].Select(true);
    }

    private void Press()
    {
        Debug.Log(selections[currentPos.x, currentPos.y].name);
        
        selections[currentPos.x, currentPos.y].Press();
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void XMove(int value)
    {
        var newPos =  new UICoord(currentPos.x + value, currentPos.y);
        Debug.Log($"x {newPos.x} {newPos.y}");
        
        
        if (IsValid(newPos))
        {
            if (newPos is {y: 1, x: 1})
            {
                newPos = new UICoord(newPos.x + value, newPos.y);
            }
            else if (newPos is {y: 1, x: 3})
            {
                newPos = new UICoord(newPos.x, newPos.y - 1);
            }
            
            selections[currentPos.y, currentPos.x].Select(false);
            currentPos = newPos;
            selections[currentPos.y, currentPos.x].Select(true);
        }
    }

    private void YMove(int value)
    {
        var newPos =  new UICoord(currentPos.x, currentPos.y + value);
        Debug.Log($"y {newPos.x} {newPos.y}");

        
        if (IsValid(newPos))
        {
            if (newPos is {y: 1, x: 1 or 3})
            {
                newPos = new UICoord(newPos.x - 1, newPos.y);
            }
            
            selections[currentPos.y, currentPos.x].Select(false);
            currentPos = newPos;
            selections[currentPos.y, currentPos.x].Select(true);
        }
    }

    private bool IsValid(UICoord coord)
    {
        if (coord.x < 0 || coord.y < 0) return false;
        if (coord.x > 3 || coord.y > 1) return false;
        
        return true;
    }
}
